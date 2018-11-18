using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Siteo.WebAPI.App_Start
{
    public class MyDBConfiguration : DbConfiguration
    {
        public MyDBConfiguration()
        {
            AddInterceptor(new SoftDeleteInterceptor());
        }
    }
}