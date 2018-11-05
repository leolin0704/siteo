using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Siteo.DAL
{
    public class BaseDAL<TEntity> where TEntity : class
    {
        //1.0实例化EF上下文容器对象
        BaseDBContext db = new BaseDBContext();

        DbSet<TEntity> _dbset;

        public BaseDAL()
        {
            //初始化
            _dbset = db.Set<TEntity>();
        }

        #region 查询
        public List<TEntity> Query(Expression<Func<TEntity, bool>> where)
        {
            return _dbset.Where(where).ToList();
        }
        #endregion

    }
}
