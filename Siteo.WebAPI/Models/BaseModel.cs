using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Siteo.WebAPI.Models
{
    public class BaseModel
    {
        public int ID { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime? LastUpdateDate { get; set; }
        public String CreateBy { get; set; }
        public DateTime LastUpdateBy { get; set; }
    }
}