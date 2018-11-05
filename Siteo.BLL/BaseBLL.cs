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

        #region  查询
        public List<TEntity> Query(Expression<Func<TEntity, bool>> where)
       {
            return bdal.Query(where);
        }

        public List<TEntity> QueryJoin(Expression<Func<TEntity, bool>> where, string[] tableNames)
        {
            return bdal.QueryJoin(where, tableNames);
        }
        #endregion

        #region  新增

        public void Add(TEntity model)
        {
            bdal.Add(model);
        }

        #endregion

        #region  编辑

        /// <summary>
        /// 要求：model必须是自己定义的实体,此时没有追加到EF容器中
        /// </summary>
        /// <param name="model"></param>
        public void Edit(TEntity model, string[] propertyName)
        {
            bdal.Edit(model, propertyName);
        }

        #endregion

        #region  物理删除

        /// <summary>
        /// model必须是自己定义的，一般是按照主键来删除
        /// </summary>
        /// <param name="model">要删除的实体对象</param>
        /// <param name="isAddedEFContext">true:表示model以及追加到了ef容器，false：未追加</param>
        public void Delete(TEntity model, bool isAddedEFContext)
        {
            bdal.Delete(model, isAddedEFContext);
        }

        #endregion

        #region 统一执行sql语句

        public int SaveChanges()
        {
            return bdal.SaveChanges();
        }

        #endregion
    }
}
