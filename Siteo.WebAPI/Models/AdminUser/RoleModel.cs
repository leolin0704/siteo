﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Siteo.WebAPI.Models.AdminUser
{
    public class RoleModel: BaseModel
    {
        public string Name
        {
            get;
            set;
        }

        public List<int> PermissionIDList {
            get;set;
        }

        public int UserCount
        {
            get;
            set;
        }
    }
}