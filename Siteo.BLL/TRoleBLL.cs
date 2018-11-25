using Siteo.Common;
using Siteo.Common.Helpers;
using Siteo.EFModel;
using System.Linq;

namespace Siteo.BLL
{
    public class TRoleBLL : BaseBLL<TRole>
    {
        const string saName = "Super Admin";

        public new void Edit(TRole model, string[] propertyName)
        {
            var duplicateRole = Query(c => (c.Name == model.Name && c.ID != model.ID));

            if (!ValidateHelper.IsNullOrEmpty(duplicateRole))
            {
                throw new ValidationException("Role name duplicated.");
            }

            var sa = Query(c => (c.Name.Equals(saName) && c.ID == model.ID));

            if (sa.Count > 0)
            {
                throw new ValidationException("Super admin could not be edited.");
            }


            base.Edit(model, propertyName);
        }

        public new void Add(TRole model)
        {
            var duplicateRole = Query(c => c.Name == model.Name);

            if (!ValidateHelper.IsNullOrEmpty(duplicateRole))
            {
                throw new ValidationException("Role name duplicated.");
            }

            base.Add(model);
        }

        public new void Delete(int roleID)
        {
            var role = Find(r => r.ID == roleID);
            if (role.TAdminUserRole != null && role.TAdminUserRole.Count > 0)
            {
                throw new ValidationException("Could not delete role when it's in use. Please remove all users under the role.");
            }

            if(string.Compare(role.Name, saName) == 0)
            {
                throw new ValidationException("Super admin could not be deleted.");
            }

            base.Delete(roleID);
        }

        public void Delete(int[] roleIDs)
        {
            var roles = Query(r => roleIDs.Contains(r.ID));
            var usedRole = roles.FirstOrDefault(r => r.TAdminUserRole != null && r.TAdminUserRole.Count > 0);
            if (usedRole != null)
            {
                throw new ValidationException("Could not delete role when it's in use. Please remove all users under the roles.");
            }

            var sa = roles.FirstOrDefault(r => r.Name.Equals(saName));

            if (sa != null)
            {
                throw new ValidationException("Super admin could not be deleted.");
            }

            base.Delete(c => roleIDs.Contains(c.ID));
        }
    }
}
