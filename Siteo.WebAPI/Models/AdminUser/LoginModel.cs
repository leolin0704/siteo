using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Siteo.WebAPI.Models.AdminUser
{
    public class LoginModel
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
    }
}