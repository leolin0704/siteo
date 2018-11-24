﻿using Siteo.EFModel;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Siteo.BLL
{
    public partial class TAdminUserBLL : BaseBLL<TAdminUser>
    {

        //public new List<TAdminUser> PagerQuery<Tkey>(int pageSize, int pageIndex, out int total, Expression<Func<TAdminUser, bool>> whereLambda, Func<TAdminUser, Tkey> orderbyLambda, bool isAsc)
        //{
        //    var adminUserList bdal.PagerQuery(pageSize, pageIndex, out total, whereLambda, orderbyLambda, isAsc);
        //}


        public void Register(TAdminUser userModel, TAdminUserRole roleModel)
        {
            userModel.Status = "A";
     
            Add(userModel);

            SaveChanges();

            roleModel.AdminUserID = userModel.ID;
       
            var roleBLL = new TAdminUserRoleBLL();
            roleBLL.Add(roleModel);
            roleBLL.SaveChanges();
        }


        public bool CheckAdminUserPermissions(TAdminUser adminUser, string[] requriedPermissionList)
        {
            if (requriedPermissionList == null || requriedPermissionList.Length == 0)
            {
                return true;
            }

            if (adminUser == null)
            {
                return false;
            }

            var adminUserRoles = adminUser.TAdminUserRole;
            if (adminUserRoles == null || adminUserRoles.Count == 0)
            {
                return false;
            }

            List<TPermission> result = GetPermissions(adminUserRoles);

            if (result.Count == 0)
            {
                return false;
            }

            foreach (var permission in result)
            {
                foreach (var reqPermissionStr in requriedPermissionList)
                {
                    if (string.Compare(permission.Name, reqPermissionStr) == 0)
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        public List<TPermission> GetPermissions(ICollection<TAdminUserRole> adminUserRoles)
        {
            var result = new List<TPermission>();
            foreach (var adminUserRole in adminUserRoles)
            {
                if (adminUserRole.TRole != null && adminUserRole.TRole.TRolePermission != null)
                {
                    foreach (var rolePermission in adminUserRole.TRole.TRolePermission)
                    {
                        if (rolePermission.TPermission != null)
                        {
                            result.Add(rolePermission.TPermission);
                        }
                    }
                }
            }

            return result;
        }
    }
}
