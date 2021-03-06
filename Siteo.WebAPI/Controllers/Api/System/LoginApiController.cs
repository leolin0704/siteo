﻿using Siteo.BLL;
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
            var adminUser = LoginManager.GetLoginUser();

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

            adminUser.LastLoginDate = DateTime.Now;
            adminUser.LastLoginIP = Request.ServerVariables.Get("Remote_Addr").ToString();
            adminUser.Token = LoginManager.GenerateToken();
            adminUser.TokenExpired = DateTime.Now.AddHours(2);
            adminUserBLL.SaveChanges();

            LoginManager.SaveLoginUser(adminUser);

         

            return Success("", new {
                Token = adminUser.Token,
                AdminUser = LoginManager.GetLoginUser(adminUser.Token)
            });
        }

        [HttpPost]
        public APIJsonResult Logout()
        {
            LoginManager.RemoveLoginUser();

            return Success();
        }
    }
}
