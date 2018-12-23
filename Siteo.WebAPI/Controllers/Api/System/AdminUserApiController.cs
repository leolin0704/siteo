using Siteo.BLL;
using Siteo.Common;
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
            var permissions = new TRoleBLL().GetPermissions(loginUser.RoleID);

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
            var adminUserBLL = new TAdminUserBLL();


            var adminUser = new TAdminUser()
            {
                Account = adminUserModel.Account.Trim(),
                Password = EncryptHelper.EncryptString(adminUserModel.Password),
                Status = adminUserModel.Status
            };

            AddCreateInfo(adminUser);

            var role = new TAdminUserRole()
            {
                RoleID = adminUserModel.RoleID
            };

            AddCreateInfo(role);
            try { 
                adminUserBLL.Register(adminUser, role);
            }
            catch (ValidationException ex)
            {
                return Failed(ex.Message);
            }

            return Success();
        }

        [HttpPost]
        public APIJsonResult Edit(AdminUserEditModel adminUserModel)
        {
            var adminUserBLL = new TAdminUserBLL();
            var adminUser = adminUserBLL.Find(u => u.ID == adminUserModel.ID);

            AddUpdateInfo(adminUser);

            List<string> updatedField = new List<string>();

            if(!string.IsNullOrEmpty(adminUserModel.Password))
            {
                adminUser.Password = EncryptHelper.EncryptString(adminUserModel.Password);
            }

            adminUser.Status = adminUserModel.Status;

            adminUserBLL.SaveChanges();

            var adminUserRoleBLL = new TAdminUserRoleBLL();
            var adminUserRole = adminUserRoleBLL.Find(r => r.AdminUserID == adminUser.ID);

            adminUserRole.RoleID = adminUserModel.RoleID;

            AddUpdateInfo(adminUserRole);
            adminUserRoleBLL.SaveChanges();

            return Success();
        }


        [HttpPost]
        public APIJsonResult Delete(int adminUserID)
        {
            var adminUserBLL = new TAdminUserBLL();
            try
            {
                adminUserBLL.Delete(adminUserID);
                adminUserBLL.SaveChanges();
            }
            catch (ValidationException ex)
            {
                return Failed(ex.Message);
            }

            var adminUserRoleBLL = new TAdminUserRoleBLL();
            adminUserRoleBLL.Delete(ur => ur.AdminUserID == adminUserID);
            adminUserRoleBLL.SaveChanges();

            return Success();
        } 

        [HttpPost]
        public APIJsonResult MultiDelete(int[] adminUserIDs)
        {

            var adminUserBLL = new TAdminUserBLL();
            var adminUserRoleIDs = adminUserBLL.Query(u => adminUserIDs.Contains(u.ID)).Select(u => u.TAdminUserRole.First().ID);

            try
            {

                adminUserBLL.Delete(adminUserIDs);
                adminUserBLL.SaveChanges();
            }
            catch(ValidationException ex)
            {
                return Failed(ex.Message);
            }

            var adminUserRoleBLL = new TAdminUserRoleBLL();
            adminUserRoleBLL.Delete(ur => adminUserRoleIDs.Contains(ur.ID));
            adminUserRoleBLL.SaveChanges();


            return Success();
        }
    }
}