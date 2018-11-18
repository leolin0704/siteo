using Siteo.Common;
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
    public class TRoleBLL : BaseBLL<TRole>
    {
        public new void Edit(TRole model, string[] propertyName)
        {
            var duplicateRole = Query(c => (c.Name == model.Name && c.ID != model.ID));

            if (!ValidateHelper.IsNullOrEmpty(duplicateRole))
            {
                throw new ValidationException("Role name duplicated.");
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
    }
}
