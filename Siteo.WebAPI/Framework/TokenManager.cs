using Siteo.BLL;
using Siteo.Common.Helpers;
using Siteo.EFModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Siteo.WebAPI.Framework
{
    public class TokenManager
    {
        public static string GenerateToken()
        {
            return Guid.NewGuid().ToString();
        }

        public static string GetRequestToken()
        {
            return HttpContext.Current.Request.Headers["token"] as string;
        }

        public static string SaveToken(TAdminUser adminUser, bool saveDB)
        {
            if (!string.IsNullOrEmpty(adminUser.Token))
            {
                CacheHelper.RemoveAllCache(adminUser.Token);
            }

            adminUser.Token = GenerateToken();
            adminUser.TokenExpired = DateTime.Now.AddHours(2);

            if (saveDB) { 
                var adminUserBLL = new TAdminUserBLL();
                adminUserBLL.Edit(adminUser, new string[] { "Token", "TokenExpired" });
                adminUserBLL.SaveChanges();
            }

            CacheHelper.SetCache(adminUser.Token, adminUser, new TimeSpan(0, 30, 0));

            return adminUser.Token;
        }

        public static TAdminUser GetLoginUser()
        {
            string token = GetRequestToken();
            if (string.IsNullOrEmpty(token))
            {
                return null;
            }

            var adminUser = CacheHelper.GetCache(token) as TAdminUser;
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