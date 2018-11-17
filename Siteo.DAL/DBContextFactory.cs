using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Siteo.DAL
{
    public static class DBContextFactory
    {
        public static BaseDBContext GetDBContext()
        {
            var context = HttpContext.Current.Application[nameof(BaseDBContext)] as BaseDBContext;
            if (context == null)
            {
                context = new BaseDBContext();
                HttpContext.Current.Application[nameof(BaseDBContext)] = context;
            }
            return context as BaseDBContext;
        }
    }
}
