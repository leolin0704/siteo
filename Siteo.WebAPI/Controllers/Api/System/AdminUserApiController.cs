using Siteo.BLL;
using Siteo.Common.Helpers;
using Siteo.EFModel;
using Siteo.WebAPI.Framework;
using Siteo.WebAPI.Models;
using Siteo.WebAPI.Models.AdminUser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Siteo.WebAPI.Controllers.Api.System
{
    public class AdminUserApiController : BaseController
    {
        [HttpGet]
        // GET api/values/5
        public APIJsonResult GetList(int pageSize, int pageIndex, string keywords)
        {
            int totalCount = 0;
            var adminUserList = new TAdminUserBLL().PagerQuery(pageSize, pageIndex, out totalCount, c => c.Account.Contains(keywords), c => c.CreateDate, false);
            return Success(new
            {
                List = UtilHelper.ConvertObjList<AdminUserModel, TAdminUser>(adminUserList),
                TotalCount = totalCount
            });
        }

        [HttpGet]
        // GET api/values/5
        public APIJsonResult GetModuleList()
        {
            var loginUser = TokenManager.GetLoginUser();
            if(loginUser == null)
            {
                return Failed("No module found.");
            }
            var permissions = new TAdminUserBLL().GetPermissions(loginUser.TAdminUserRole);

            var moduleBLL = new TModuleBLL();
            var modules = moduleBLL.GetUserModules(permissions);

            var moduleModels = UtilHelper.ConvertObjList<ModuleModel, TModule>(modules);

            for (int i = 0; i < moduleModels.Count; i++)
            {
                var currentModuleModel = moduleModels[i];
                var currentModule = modules[i];

                currentModuleModel.ChildModules = UtilHelper.ConvertObjList<ModuleModel, TModule>(currentModule.TModule1);

            }

            return Success(new
            {
                Modules = moduleModels
            });
        }
    }
}