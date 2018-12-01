using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Siteo.WebAPI.Models.AdminUser
{
    public class NoticeModel : BaseModel
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "Title is required.")]
        [MaxLength(200, ErrorMessage = "Title must be less than 200 charactors.")]
        public string Title
        {
            get;
            set;
        }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Content is required.")]
        [MaxLength(4000, ErrorMessage = "Content must be less than 4000 charactors.")]
        public string Content
        {
            get;
            set;
        }
    }
}