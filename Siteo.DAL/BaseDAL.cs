using Siteo.Common.Helpers;
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
            return _dbset
                //.Where(LambdaHelper.CreateEqual<TEntity>("IsDeleted", 0))
                .Where(where).ToList();
        }


        /// <summary>
        /// 分页查询 + 条件查询 + 排序
        /// </summary>
        /// <typeparam name="Tkey">泛型</typeparam>
        /// <param name="pageSize">每页大小</param>
        /// <param name="pageIndex">当前页码</param>
        /// <param name="total">总数量</param>
        /// <param name="whereLambda">查询条件</param>
        /// <param name="orderbyLambda">排序条件</param>
        /// <param name="isAsc">是否升序</param>
        /// <returns>IQueryable 泛型集合</returns>
        public IQueryable<TEntity> PagerQuery<Tkey>(int pageSize, int pageIndex, out int total, Expression<Func<TEntity, bool>> whereLambda, Func<TEntity, Tkey> orderbyLambda, bool isAsc)
        {
            total = db.Set<TEntity>().Where(whereLambda).Count();
            if (isAsc)
            {
                var temp = db.Set<TEntity>().Where(whereLambda)
                             .OrderBy<TEntity, Tkey>(orderbyLambda)
                             .Skip(pageSize * (pageIndex - 1))
                             .Take(pageSize);
                return temp.AsQueryable();
            }
            else
            {
                var temp = db.Set<TEntity>().Where(whereLambda)
                           .OrderByDescending<TEntity, Tkey>(orderbyLambda)
                           .Skip(pageSize * (pageIndex - 1))
                           .Take(pageSize);
                return temp.AsQueryable();
            }
        }

        public TEntity Find(Expression<Func<TEntity, bool>> where)
        {
            return _dbset.AsQueryable()
                //.Where(LambdaHelper.CreateEqual<TEntity>( "IsDeleted", 0 ))
                .FirstOrDefault(where);
        }


        //public List<TEntity> QueryJoin(Expression<Func<TEntity, bool>> where, string[] tableNames)
        //{
        //    //将子类_dbset 赋值给父类的query
        //    DbQuery<TEntity> query = _dbset;

        //    foreach (var item in tableNames)
        //    {
        //        //遍历要连表的表名称，最终得到所有连表以后的DbQuery对象
        //        query = query.Include(item);
        //    }
        //    return query.Where(where).ToList();
        //}

        #endregion

        #region 新增
        public void Add(TEntity model)
        {
            var type = model.GetType();
            var isDeletedProperty = type.GetProperty("IsDeleted");
            
            isDeletedProperty.SetValue(model, 0, null);

            _dbset.Add(model);
        }
        #endregion

        #region 编辑
        public void Edit(TEntity model, string[] propertyName)
        {
            Edit(model, propertyName, true);
        }
        public void Edit(TEntity model, string[] propertyName, bool updateUpdateInfo)
        {
            if (model == null)
            {
                throw new Exception("model could not be null");
            }
            if (propertyName == null || propertyName.Any() == false)
            {
                throw new Exception("No property has been changed.");
            }

            var type = model.GetType();

            //将model追加到EF容器
            DbEntityEntry entry = db.Entry(model);

            entry.State = EntityState.Unchanged;

            if (updateUpdateInfo)
            {
                entry.Property("LastUpdateBy").IsModified = true;
                entry.Property("LastUpdateDate").IsModified = true;
            }

            foreach (var item in propertyName)
            {
                entry.Property(item).IsModified = true;
            }
        }
        #endregion


        #region Delete
        public void Delete(Expression<Func<TEntity, bool>> where)
        {
            if (where == null)
            {
                throw new Exception("Delete need conditions");
            }

            var modelList = _dbset.AsQueryable().Where(where);

            foreach (var model in modelList)
            {
                Delete(model);
            }
        }

        public void Delete(int id)
        {
            if (id == 0)
            {
                throw new Exception("id could not be 0");
            }

            var model = _dbset.AsQueryable().Where(LambdaHelper.CreateEqual<TEntity>("ID", id)).FirstOrDefault();

            Delete(model);
        }

        public void Delete(TEntity model)
        {
            if (model == null)
            {
                throw new Exception("model could not be null");
            }

            //将model追加到EF容器
            DbEntityEntry entry = db.Entry(model);

            entry.State = EntityState.Unchanged;

            entry.Property("IsDeleted").IsModified = true;
            entry.Property("IsDeleted").CurrentValue = 1;
        }
        #endregion

        #region delete
        ////EntityState.Unchanged
        //public void Delete(TEntity model, bool isAddedEFContext)
        //{
        //    if (isAddedEFContext == false)
        //    {
        //        _dbset.Attach(model);
        //    }
        //    //修改状态为deleted
        //    _dbset.Remove(model);
        //}
        #endregion

        #region 统一执行保存
        public int SaveChanges()
        {
            return db.SaveChanges();
        }
        #endregion
    }
}
