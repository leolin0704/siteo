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

            var adminUserModelList = UtilHelper.ConvertObjList<TAdminUser, AdminUserModel>(adminUserList);

            for (int i = 0; i < adminUserModelList.Count; i++)
            {
                var adminUserModel = adminUserModelList[i];
                var adminUser = adminUserList[i];
                adminUserModel.Role = UtilHelper.CopyProperties<RoleModel>(adminUser.TAdminUserRole.First().TRole);
            }


            return Success("",new
            {
                List = adminUserModelList,
                TotalCount = totalCount
            });
        }


        [HttpGet]
        // GET api/values/5
        public APIJsonResult Get(int id)
        {
            var adminUser = new TAdminUserBLL().Find(c => c.ID == id);
            var adminUserModel = UtilHelper.CopyProperties<AdminUserModel>(adminUser);
            adminUserModel.RoleID = adminUser.TAdminUserRole.FirstOrDefault().RoleID;

            return Success("",
                    new
                    {
                        Data = adminUserModel
                    }
                );
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

            var moduleModels = UtilHelper.ConvertObjList<TModule, ModuleModel>(modules);

            UtilHelper.ConvertChildObjList<TModule, ModuleModel, TModule, ModuleModel>(modules, moduleModels, "TModule1", "ChildModules");

            return Success("",new
            {
                Modules = moduleModels
            });
        }


        [HttpPost]
        public APIJsonResult Add(AdminUserRegModel adminUserModel)
        {
            var adminUser = new TAdminUser()
            {
                Account = adminUserModel.Account,
                Password = EncryptHelper.Encrypt(adminUserModel.Password),
            };

            AddCreateInfo(adminUser);

            var role = new TAdminUserRole()
            {
                RoleID = adminUserModel.RoleID
            };

            AddCreateInfo(role);

            new TAdminUserBLL().Register(adminUser, role);

            return Success();
        }
    }
}