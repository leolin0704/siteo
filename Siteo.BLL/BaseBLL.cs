using Siteo.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Siteo.BLL
{
    public class BaseBLL<TEntity> where TEntity : class
   {
       //初始化BaseDal泛型类的对象
       BaseDAL<TEntity> bdal = new BaseDAL<TEntity>();

       public List<TEntity> Query(Expression<Func<TEntity, bool>> where)
       {
            return bdal.Query(where);
        }
    }
}
