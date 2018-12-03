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
    public class NoticeApiController : BaseController
    {
        [HttpGet]
        // GET api/values/5
        public APIJsonResult GetList(int pageSize, int pageIndex, string keywords)
        {
            int totalCount = 0;
            var noticeList = new TNoticeBLL().PagerQuery(pageSize, pageIndex, out totalCount, c => c.Title.Contains(keywords) || c.Content.Contains(keywords), c => c.CreateDate, false);
            var noticeModelList = UtilHelper.ConvertObjList<TNotice, NoticeModel>(noticeList);

            return Success("", new
            {
                List = noticeModelList.Select(c => {
                    c.Content = "";
                    return c;
                }).ToList(),
                TotalCount = totalCount
            });
        }

        [HttpGet]
        // GET api/values/5
        public APIJsonResult Get(int id)
        {
            var notice = new TNoticeBLL().Find(c => c.ID == id);
            var moticeModel = UtilHelper.CopyProperties<NoticeModel>(notice);

            return Success("",
                new
                {
                    Data = moticeModel
                }
                );
        }

        [HttpPost]
        // GET api/values/5
        public APIJsonResult Add(NoticeModel noticeModel)
        {
            var noticeBLL = new TNoticeBLL();
            var notice = new TNotice();
            UtilHelper.CopyProperties(noticeModel, notice);

            AddCreateInfo(notice);

            noticeBLL.Add(notice);

            noticeBLL.SaveChanges();

            return Success();
        }

        [HttpPost]
        public APIJsonResult Edit(NoticeModel noticeModel)
        {
            var noticeBLL = new TNoticeBLL();
            var notice = new TNotice();
            UtilHelper.CopyProperties(noticeModel, notice);

            AddUpdateInfo(notice);

            noticeBLL.Edit(notice, new string[] { "Title", "Content" });

            noticeBLL.SaveChanges();

            return Success();
        }

        [HttpPost]
        public APIJsonResult Delete(int noticeID)
        {
            var noticeBLL = new TNoticeBLL();

            noticeBLL.Delete(noticeID);

            noticeBLL.SaveChanges();

            return Success();
        }

        [HttpPost]
        public APIJsonResult MultiDelete(int[] noticeIDs)
        {

            var noticeBLL = new TNoticeBLL();

            noticeBLL.Delete(noticeIDs);

            noticeBLL.SaveChanges();

            return Success();
        }

    }
}