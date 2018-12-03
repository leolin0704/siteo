using Siteo.EFModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Siteo.BLL
{
    public class TNoticeBLL : BaseBLL<TNotice>
    {
        public void Delete(int[] noticeIDs)
        {

            base.Delete(c => noticeIDs.Contains(c.ID));
        }

    }
}
