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
    public class RoleApiController : BaseController
    {
        [HttpGet]
        // GET api/values/5
        public APIJsonResult GetList(int pageSize, int pageIndex, string keywords)
        {
            int totalCount = 0;
            var roleList = new TRoleBLL().PagerQuery(pageSize, pageIndex, out totalCount, c => c.Name.Contains(keywords), c => c.CreateDate, true);
            var roleModelList = UtilHelper.ConvertObjList<TRole, RoleModel>(roleList);

            for(var i =0; i < roleModelList.Count; i++)
            {
                var roleModel = roleModelList[i];
                var role = roleList[i];
                roleModel.UserCount = role.TAdminUserRole.Count;
            }

            return SuccessList("", roleModelList, totalCount);
        }

        [HttpGet]
        // GET api/values/5
        public APIJsonResult Get(int id)
        {
            var role = new TRoleBLL().Find(c => c.ID == id);
            var roleModel = UtilHelper.CopyProperties<RoleModel>(role);
            if (role.TRolePermission != null && role.TRolePermission.Count > 0)
            {
                roleModel.PermissionIDList = role.TRolePermission.Select(c => c.PermissionID).ToList();
            }

            return Success("",
                new
                {
                    Data = roleModel
                }
                );
        }

        [HttpGet]
        public APIJsonResult GetPermissionList()
        {
            var permissionList = new TPermissionBLL().Query(c => true);
            return SuccessList<PermissionModel, TPermission>("", permissionList, null);
        }

        [HttpPost]
        public APIJsonResult Delete(int roleID)
        {
            var roleBLL = new TRoleBLL();

            try { 
                roleBLL.Delete(roleID);
            }catch(ValidationException ex)
            {
                return Failed(ex.Message);
            }

            roleBLL.SaveChanges();

            return Success();
        }

        [HttpPost]
        public APIJsonResult MultiDelete(int[] roleIDs)
        {
           
            var roleBLL = new TRoleBLL();

            try
            {
                roleBLL.Delete(roleIDs);
            }
            catch (ValidationException ex)
            {
                return Failed(ex.Message);
            }

            roleBLL.SaveChanges();

            return Success();
        }

        [HttpPost]
        public APIJsonResult Edit(RoleModel roleModel)
        {
            var roleBLL = new TRoleBLL();
            var role = new TRole();
            UtilHelper.CopyProperties(roleModel, role);
            AddUpdateInfo(role);

            try { 
                roleBLL.Edit(role, new string[] { "Name" });
            }
            catch (ValidationException ex)
            {
                return Failed(ex.Message);
            }

            roleBLL.SaveChanges();

            var rolePermissionBLL = new TRolePermissionBLL();
            rolePermissionBLL.Delete(c => c.RoleID == role.ID);

            if (roleModel.PermissionIDList != null && roleModel.PermissionIDList.Count > 0)
            {
                foreach (var permissionID in roleModel.PermissionIDList)
                {
                    var rolePermission = new TRolePermission()
                    {
                        PermissionID = permissionID,
                        RoleID = role.ID
                    };

                    AddCreateInfo(rolePermission);

                    rolePermissionBLL.Add(rolePermission);
                }
            }

            rolePermissionBLL.SaveChanges();


            return Success();
        }

        [HttpPost]
        // GET api/values/5
        public APIJsonResult Add(RoleModel roleModel)
        {
            roleModel.Name = roleModel.Name.Trim();
            var roleBLL = new TRoleBLL();
            var role = new TRole();
            UtilHelper.CopyProperties(roleModel, role);

            AddCreateInfo(role);

            try
            {
                roleBLL.Add(role);
            }
            catch (ValidationException ex)
            {
                return Failed(ex.Message);
            }

            roleBLL.SaveChanges();

            if (roleModel.PermissionIDList !=null && roleModel.PermissionIDList.Count > 0)
            {
                var rolePermissionBLL = new TRolePermissionBLL();

                foreach (var permissionID in roleModel.PermissionIDList)
                {
                    var rolePermission = new TRolePermission()
                    {
                        PermissionID = permissionID,
                        RoleID = role.ID
                    };

                    AddCreateInfo(rolePermission);

                    rolePermissionBLL.Add(rolePermission);
                }

                rolePermissionBLL.SaveChanges();
            }

          
            return Success();
        }
    }
}