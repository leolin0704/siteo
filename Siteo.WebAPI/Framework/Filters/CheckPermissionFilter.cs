using Siteo.BLL;
using Siteo.Common.Helpers;
using Siteo.EFModel;
using Siteo.WebAPI.Attributes.Permissions;
using Siteo.WebAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace Siteo.WebAPI.Framework.Filters
{
    public class CheckPermissionFilter : FilterAttribute, IAuthorizationFilter
    {
        /// <summary>
        /// 权限判断
        /// </summary>
        /// <param name="filterContext"></param>
        public void OnAuthorization(AuthorizationContext filterContext)
        {
            if (filterContext == null)
            {
                throw new ArgumentNullException("filterContext");
            }
            if (filterContext.HttpContext.Request.Url == null)
            {
                throw new ArgumentNullException("filterContext");
            }
            string pageUrl = filterContext.HttpContext.Request.Url.AbsolutePath; //OperateContext.GetThisPageUrl(false);

            // 允许匿名访问 用于标记在授权期间要跳过 AuthorizeAttribute 的控制器和操作的特性 
            var actionAnonymous = filterContext.ActionDescriptor.GetCustomAttributes(typeof(AllowAnonymousAttribute), true) as IEnumerable<AllowAnonymousAttribute>;
            var controllerAnonymous = filterContext.Controller.GetType().GetCustomAttributes(typeof(AllowAnonymousAttribute), true) as IEnumerable<AllowAnonymousAttribute>;
            if ((actionAnonymous != null && actionAnonymous.Any()) || (controllerAnonymous != null && controllerAnonymous.Any()))
            {
                return;
            }

            //url获取token
            var content = filterContext.HttpContext;
            var adminUser = TokenManager.GetLoginUser();

            if (adminUser == null) { // not logined
                ProcessNotLogin(filterContext);
                return;
            }

            var permissionAttr = filterContext.ActionDescriptor.GetCustomAttributes(typeof(PermissionAttribute), true) as IEnumerable<PermissionAttribute>;

            if (permissionAttr == null || permissionAttr.Count() == 0) {
                return;
            }

            var hasPermission = new TAdminUserBLL().CheckAdminUserPermissions(adminUser, permissionAttr.ToList()[0].PermissionList);
            if (!hasPermission) {
                ProcessNoPermission(filterContext);
            }
        }

        private static void ProcessNoPermission(AuthorizationContext filterContext)
        {
            var jsonResult = new JsonResult();
            jsonResult.Data = new ResponseResult(ResponseResultStatus.NO_PERMISSION, "No permission");
            jsonResult.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
            filterContext.Result = jsonResult;
        }

        private static void ProcessNotLogin(AuthorizationContext filterContext)
        {
            var jsonResult = new JsonResult();
            jsonResult.Data = new ResponseResult(ResponseResultStatus.NOT_LOGIN, "Not login");
            jsonResult.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
            filterContext.Result = jsonResult;
        }
    }
}