using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Siteo.WebAPI.Framework.Filters
{
    public class AllowCrossSiteJsonFilter : ActionFilterAttribute
    {
        public string[] AllowSites { get; set; }
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            onExcute(filterContext, AllowSites);
            base.OnActionExecuting(filterContext);
        }


        public void onExcute(ControllerContext context, string[] AllowSites)
        {
            var origin = context.HttpContext.Request.Headers["Origin"];
            Action action = () =>
            {
                context.HttpContext.Response.AppendHeader("Access-Control-Allow-Origin", origin);

            };
            if (AllowSites != null && AllowSites.Any())
            {
                if (AllowSites.Contains(origin))
                {
                    action();
                }
            }

        }
    }
}