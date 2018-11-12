using Siteo.BLL;
using Siteo.Common.Helpers;
using Siteo.WebAPI.Attributes.Permissions;
using Siteo.WebAPI.Framework;
using System;
using System.Web.Mvc;
using Siteo.WebAPI.Models.AdminUser;
using Siteo.EFModel;

namespace Siteo.WebAPI.Controllers.Api.AdminUser
{
    public class LoginApiController : BaseController
    {
        private TAdminUserBLL adminUserBLL = new TAdminUserBLL();

        [Permission("banner管理")]
        [HttpGet]
        // GET api/values/5
        public JsonResult Get(int id)
        {
            return Json(1, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        // GET api/values/5
        public JsonResult GetLoginUser()
        {
            var adminUser = Session["adminUser"] as TAdminUser;
            var sessionUserModel = new AdminUserModel();
            UtilHelper.CopyProperties(adminUser, sessionUserModel, new string[] {
                "Account" ,
                "Avatar",
                "LastLoginDate",
                "LastLoginIP"
            });
            return Success(new {
                AdminUser = adminUser
            });
        }

        [HttpPost]
        public JsonResult Login(LoginModel adminUserModel)
        {
            var adminUser  = adminUserBLL.Find(user =>  user.Account == adminUserModel.Account);

            if (adminUser == null || !UtilHelper.CompareByte(adminUser.Password, EncryptHelper.Encrypt(adminUserModel.Password))) {
                return Failed("Account and password do not match.");
            }

            adminUser.LastLoginDate = DateTime.Now;
            adminUser.LastLoginIP = Request.ServerVariables.Get("Remote_Addr").ToString();
            adminUserBLL.Edit(adminUser, new string[] {
                "LastLoginDate", "LastLoginIP"
            });
            adminUserBLL.SaveChanges();

            var token = Guid.NewGuid().ToString();
            Session["token"] = token;
            
            Session["adminUser"] = adminUser;
            return Success(new {
                Token = token
            });
        }


        [HttpPost]
        public JsonResult Add(AdminUserModel adminUserModel)
        {
            var adminUser = new TAdminUser()
            {
                Account = adminUserModel.Account,
                Password = EncryptHelper.Encrypt(adminUserModel.Password),
            };

            var role = new TAdminUserRole()
            {
                RoleID = adminUserModel.RoleID
            };
            new TAdminUserBLL().Register(adminUser, role);

            return Success();
        }
    }
}
