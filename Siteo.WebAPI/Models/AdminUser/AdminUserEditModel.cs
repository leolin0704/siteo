using Siteo.WebAPI.Framework;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Siteo.WebAPI.Models.AdminUser
{
    public class AdminUserEditModel : BaseModel
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "Account is required.")]
        [MinLength(5, ErrorMessage = "Account must be more than 5 charactors.")]
        [MaxLength(30, ErrorMessage = "Account must be less than 30 charactors.")]
        public string Account
        {
            get;
            set;
        }
        
        [MinLength(5, ErrorMessage = "Password must be more than 5 charactors.")]
        [MaxLength(30, ErrorMessage = "Password must be less than 30 charactors.")]
        public string Password
        {
            get;
            set;
        }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Status is required.")]
        [EnumDataType(typeof(AdminUserStatusList), ErrorMessage = "Status is invalid.")]
        public string Status
        {
            get;
            set;
        }

        [Range(1,int.MaxValue,ErrorMessage = "Role is required.")]
        public int RoleID
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