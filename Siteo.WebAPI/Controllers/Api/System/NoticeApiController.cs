﻿using Siteo.BLL;
using Siteo.WebAPI.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Siteo.WebAPI.Controllers.Api.System
{
    public class NoticeApiController : BaseController
    {
        [HttpGet]
        // GET api/values/5
        public JsonResult GetList(int pageSize, int pageIndex)
        {
            int totalCount = 0;
            var noticeList = new TNoticeBLL().PagerQuery(pageSize, pageIndex, out totalCount, c => true, c => c.CreateDate, false);
            return Success(new
            {
                NoticeList = noticeList,
                TotalCount = totalCount
            });
        }
    }
}