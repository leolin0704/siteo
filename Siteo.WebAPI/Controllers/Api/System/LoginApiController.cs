using Siteo.BLL;
using Siteo.Common.Helpers;
using Siteo.WebAPI.Attributes.Permissions;
using Siteo.WebAPI.Framework;
using System;
using System.Web.Mvc;
using Siteo.WebAPI.Models.AdminUser;
using Siteo.EFModel;
using Siteo.WebAPI.Models;
using Siteo.Common.Consts;

namespace Siteo.WebAPI.Controllers.Api.System
{
    public class LoginApiController : BaseController
    {
        private TAdminUserBLL adminUserBLL = new TAdminUserBLL();

        [HttpGet]
        // GET api/values/5
        public APIJsonResult GetLoginUser()
        {
            var adminUser = TokenManager.GetLoginUser();

            return Success("", new {
                AdminUser = adminUser
            });
        }

        [HttpPost]
        [AllowAnonymous]
        public APIJsonResult Login(LoginModel adminUserModel)
        {
            var adminUser  = adminUserBLL.Find(user =>  user.Account == adminUserModel.Account && user.Status == UserStatusList.Active);

            if (adminUser == null || !UtilHelper.CompareString(adminUser.Password, EncryptHelper.EncryptString(adminUserModel.Password))) {
                return Failed("Account and password do not match.");
            }

            var token = TokenManager.SaveToken(adminUser, false);

            adminUser.LastLoginDate = DateTime.Now;
            adminUser.LastLoginIP = Request.ServerVariables.Get("Remote_Addr").ToString();
            adminUserBLL.SaveChanges();

            return Success("", new {
                Token = token,
                AdminUser = TokenManager.GetLoginUser(token)
            });
        }

        [HttpPost]
        public APIJsonResult Logout()
        {
            TokenManager.RemoveLoginUser();

            return Success();
        }
    }
}
