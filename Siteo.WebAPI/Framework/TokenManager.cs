using Siteo.BLL;
using Siteo.Common.Helpers;
using Siteo.EFModel;
using Siteo.WebAPI.Models.AdminUser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Siteo.WebAPI.Framework
{
    public class LoginManager
    {
        public static string GenerateToken()
        {
            return Guid.NewGuid().ToString();
        }

        public static string GetRequestToken()
        {
            return HttpContext.Current.Request.Headers["token"] as string;
        }

        public static void SaveLoginUser(TAdminUser adminUser)
        {
            if (!string.IsNullOrEmpty(adminUser.Token))
            {
                CacheHelper.RemoveAllCache(adminUser.Token);
            }

            var sessionUserModel = new AdminUserModel();
            UtilHelper.CopyProperties(adminUser, sessionUserModel, new string[] {
                "ID",
                "Account" ,
                "Avatar",
                "Token",
                "TokenExpired",
                "LastLoginDate",
                "LastLoginIP"
            });

            var role = new RoleModel();
            var adminUserRoleBLL = new TAdminUserRoleBLL();
            var adminUserRole = adminUserRoleBLL.Find(ur => ur.AdminUserID == sessionUserModel.ID);
            UtilHelper.CopyProperties(adminUserRole.TRole, role, new string[] {
                "ID",
                "Name"
            });

            sessionUserModel.RoleID = role.ID;
            sessionUserModel.Role = role;

            CacheHelper.SetCache(adminUser.Token, sessionUserModel, new TimeSpan(0, 30, 0));
        }

        public static void RemoveLoginUser()
        {
            string token = GetRequestToken();
            var adminUserBLL = new TAdminUserBLL();
            var refreshAdminUser = adminUserBLL.Find(u => u.Token == token);
            if (refreshAdminUser != null)
            {
                refreshAdminUser.Token = string.Empty;
                refreshAdminUser.TokenExpired = null;
                adminUserBLL.SaveChanges();
            }

            CacheHelper.RemoveAllCache(token);
        }

        public static AdminUserModel GetLoginUser()
        {
            string token = GetRequestToken();
            return GetLoginUser(token);
        }

        public static AdminUserModel GetLoginUser(string token)
        {
            if (string.IsNullOrEmpty(token))
            {
                return null;
            }

            var adminUser = CacheHelper.GetCache(token) as AdminUserModel;
            if(adminUser == null)
            {
                return null;
            }

            if(adminUser.TokenExpired < DateTime.Now.AddMinutes(30))
            {
                var adminUserBLL = new TAdminUserBLL();
                var refreshAdminUser = adminUserBLL.Find(u => (u.Token == adminUser.Token && u.TokenExpired > DateTime.Now));
                if (refreshAdminUser != null) {
                    refreshAdminUser.TokenExpired = DateTime.Now.AddHours(2);
                    adminUserBLL.Edit(refreshAdminUser, new string[] { "TokenExpired" }, false);
                    adminUserBLL.SaveChanges();

                    CacheHelper.SetCache(refreshAdminUser.Token, refreshAdminUser, new TimeSpan(0, 30, 0));
                }
                else{
                    CacheHelper.RemoveAllCache(adminUser.Token);
                    return null;
                }


            }

            return adminUser;
        }
    }
}