using Siteo.BLL;
using Siteo.Common.Helpers;
using Siteo.EFModel;
using Siteo.WebAPI.Framework;
using Siteo.WebAPI.Models;
using Siteo.WebAPI.Models.AdminUser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Siteo.WebAPI.Controllers.Api.System
{
    public class RoleApiController : BaseController
    {
        [HttpGet]
        // GET api/values/5
        public APIJsonResult GetList(int pageSize, int pageIndex, string keywords)
        {
            int totalCount = 0;
            var roleList = new TRoleBLL().PagerQuery(pageSize, pageIndex, out totalCount, c => c.Name.Contains(keywords), c => c.CreateDate, false);
            return Success(new
            {
                List = UtilHelper.ConvertObjList<RoleModel,TRole>(roleList),
                TotalCount = totalCount
            });
        }
    }
}