using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Siteo.WebAPI.Models.AdminUser
{
    public class ModuleModel : BaseModel
    {
        public string Name { get; set; }
        public string Key { get; set; }
        public List<ModuleModel> ChildModules { get; set; }
    }
}