using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
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

        public List<TEntity> QueryJoin(Expression<Func<TEntity, bool>> where, string[] tableNames)
        {
            //将子类_dbset 赋值给父类的query
            DbQuery<TEntity> query = _dbset;

            foreach (var item in tableNames)
            {
                //遍历要连表的表名称，最终得到所有连表以后的DbQuery对象
                query = query.Include(item);
            }
            return query.Where(where).ToList();
        }

        #endregion

        #region 新增
        public void Add(TEntity model)
        {
            _dbset.Add(model);
        }
        #endregion

        #region 编辑
        public void Edit(TEntity model, string[] propertyName)
        {
            if (model == null)
            {
                throw new Exception("model必须为实体的对象");
            }
            if (propertyName == null || propertyName.Any() == false)
            {
                throw new Exception("必须至少指定一个要修改的属性");
            }

            //将model追加到EF容器
            DbEntityEntry entry = db.Entry(model);

            entry.State = EntityState.Unchanged;

            foreach (var item in propertyName)
            {
                entry.Property(item).IsModified = true;
            }
        }
        #endregion

        #region 物理删除
        //EntityState.Unchanged
        public void Delete(TEntity model, bool isAddedEFContext)
        {
            if (isAddedEFContext == false)
            {
                _dbset.Attach(model);
            }
            //修改状态为deleted
            _dbset.Remove(model);
        }
        #endregion

        #region 统一执行保存
        public int SaveChanges()
        {
            return db.SaveChanges();
        }
        #endregion
    }
}
