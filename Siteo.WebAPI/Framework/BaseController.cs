using Siteo.Common.Helpers;
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
        public void AddCreateInfo<TEntity>(TEntity model)
        {
            var type = model.GetType();
            var createByProperty = type.GetProperty("CreateBy");
            var createDateProperty = type.GetProperty("CreateDate");

            var loginUser = TokenManager.GetLoginUser();

            if(loginUser == null)
            {
                return;
            }

            createByProperty.SetValue(model, loginUser.Account, null);
            createDateProperty.SetValue(model, DateTime.Now, null);
        }

        public void AddUpdateInfo<TEntity>(TEntity model)
        {
            var type = model.GetType();
            var lastUpdateByProperty = type.GetProperty("LastUpdateBy");
            var lastUpdateDateProperty = type.GetProperty("LastUpdateDate");

            var loginUser = TokenManager.GetLoginUser();

            if (loginUser == null)
            {
                return;
            }

            lastUpdateByProperty.SetValue(model, loginUser.Account, null);
            lastUpdateDateProperty.SetValue(model, DateTime.Now, null);
        }

        public APIJsonResult Success()
        {
            return Success(null);
        }

        public APIJsonResult Success(object data)
        {
            var jsonResult = new APIJsonResult();
            jsonResult.Data = new ResponseResult(ResponseResultStatus.SUCCESS, "Operation suceeded.", data);
            jsonResult.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
            return jsonResult;
        }

        public APIJsonResult SuccessList<TTarget>(List<TTarget> list, int? totalCount)
        {
            return Success(new
            {
                List = list,
                TotalCount = totalCount
            });
        }

        public APIJsonResult SuccessList<TTarget, TSource>(List<TSource> data, int? totalCount) where TTarget : new()
        {
            return Success(new
            {
                List = UtilHelper.ConvertObjList<TSource, TTarget>(data),
                TotalCount = totalCount
            });
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