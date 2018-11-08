using Siteo.WebAPI.Framework.Filters;
using Siteo.WebAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Siteo.WebAPI.Framework
{
    [CheckPermissionFilter]
    public class BaseController: Controller
    {
        public JsonResult Success()
        {
            return Success(null);
        }

        public JsonResult Success(Object data)
        {
            var jsonResult = new JsonResult();
            jsonResult.Data = new ResponseResult(1, "Sucess", data);
            jsonResult.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
            return jsonResult;
        }


        public JsonResult Failed(string message)
        {
            var jsonResult = new JsonResult();
            jsonResult.Data = new ResponseResult(2, message);
            jsonResult.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
            return jsonResult;
        }
    }
}