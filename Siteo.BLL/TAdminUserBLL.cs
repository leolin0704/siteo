using Siteo.Common;
using Siteo.EFModel;
using System.Collections.Generic;
using System.Linq;

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

            var existAccount = Find(u => u.Account == userModel.Account);

            if (existAccount != null)
            {
                throw new ValidationException("Account name duplicated.");
            }
     
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


        private string saAccount = "admin";

        public new void Delete(int adminID)
        {
            var adminUser = Find(r => r.ID == adminID);

            if (string.Compare(adminUser.Account, saAccount) == 0)
            {
                throw new ValidationException("Admin could not be deleted.");
            }

            base.Delete(adminID);
        }

        public void Delete(int[] adminUserIDs)
        {
            var sa = Query(r => adminUserIDs.Contains(r.ID)).FirstOrDefault(r => r.Account.Equals(saAccount));

            if (sa != null)
            {
                throw new ValidationException("Admin could not be deleted.");
            }

            base.Delete(c => adminUserIDs.Contains(c.ID));
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
