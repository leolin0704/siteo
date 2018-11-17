using Siteo.Common.Helpers;
using Siteo.EFModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Siteo.BLL
{
    public class TModuleBLL : BaseBLL<TModule>
    {
        public List<TModule> GetUserModules(List<TPermission> permissions)
        {
            var permissionIDs = permissions.Select(p =>
            {
                return p.ID;
            });

            return bdal.Query(c => (!c.ParentModuleID.HasValue && c.PermissionID.HasValue && permissionIDs.Contains(c.PermissionID.Value))).OrderByDescending(c => c.Order).ToList();
        }


    }
}
