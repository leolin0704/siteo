using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Siteo.WebAPI.Models.AdminUser
{
    public class AdminUserModel : BaseModel
    {
        public string Account
        {
            get;
            set;
        }
        
        public string Status
        {
            get;
            set;
        }
        
        public int RoleID
        {
            get;
            set;
        }


        public RoleModel Role
        {
            get;
            set;
        }
        
        public DateTime? LastLoginDate
        {
            get;
            set;
        }

        public string LastLoginIP
        {
            get;
            set;
        }

        public string Avatar
        {
            get;
            set;
        }
    }
}