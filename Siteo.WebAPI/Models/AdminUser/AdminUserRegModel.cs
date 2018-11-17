﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Siteo.WebAPI.Models.AdminUser
{
    public class AdminUserRegModel : BaseModel
    {
        [Required(ErrorMessage = "Account is required.")]
        public string Account
        {
            get;
            set;
        }

        [Required(ErrorMessage = "Password is required.")]
        public string Password
        {
            get;
            set;
        }
        
        public int RoleID
        {
            get;
            set;
        }
        
        public DateTime LastLoginDate
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