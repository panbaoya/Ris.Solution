using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Ris.Dal
{
    public interface IRepository<T> where T : class
    {
        /// <summary>
        /// 新增实体
        /// </summary>
        /// <param name="entity">entity</param>
        int Insert(T entity);

        /// <summary>
        /// 删除实体
        /// </summary>
        /// <param name="entity">entity</param>
        int Delete(T entity);

        /// <summary>
        /// 修改实体
        /// </summary>
        /// <param name="entity">entity</param>
        int Update(T entity);

        /// <summary>
        /// 根据id获取实体
        /// </summary>
        /// <param name="id">id</param>
        /// <returns>T</returns>
        T GetById(object id);

        /// <summary>
        /// 根据表达式获取实体
        /// </summary>
        /// <param name="lambda">lambda</param>
        /// <returns>T</returns>
        T GetModel(Expression<Func<T, bool>> lambda);

        /// <summary>
        /// 根据sql语句获取实体
        /// </summary>
        /// <param name="sql">sql语句</param>
        /// <param name="param">参数列表</param>
        /// <returns></returns>
        T GetModelBySql(string sql, params object[] param);

        /// <summary>
        /// 根据表达式获取列表
        /// </summary>
        /// <param name="lambda"></param>
        /// <returns></returns>
        List<T> GetList(Expression<Func<T, bool>> lambda);

        /// <summary>
        /// 根据sql获取列表T
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        List<T> GetListBySql(string sql, params object[] parameters);

        /// <summary>
        /// 根据sql获取指定实体列表F
        /// </summary>
        /// <typeparam name="F"></typeparam>
        /// <param name="sql"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        List<F> GetListBySql<F>(string sql, params object[] parameters);
    }
}