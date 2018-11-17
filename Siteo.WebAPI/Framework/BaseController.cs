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
        public APIJsonResult Success()
        {
            return Success(null);
        }

        public APIJsonResult Success(object data)
        {
            var jsonResult = new APIJsonResult();
            jsonResult.Data = new ResponseResult(ResponseResultStatus.SUCCESS, "Sucess", data);
            jsonResult.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
            return jsonResult;
        }


        public APIJsonResult Failed(string message)
        {
            var jsonResult = new APIJsonResult();
            jsonResult.Data = new ResponseResult(ResponseResultStatus.FAILED, message);
            jsonResult.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
            return jsonResult;
        }
    }
}