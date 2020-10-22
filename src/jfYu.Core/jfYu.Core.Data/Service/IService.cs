using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace jfYu.Core.Data
{
    public interface IService<T> where T : BaseEntity
    {
        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="entity">数据</param>
        /// <returns>结果</returns>
        bool Add(T entity);
        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="entity">数据</param>
        /// <returns>结果</returns>
        Task<bool> AddAsync(T entity);

        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="entity">列表</param>
        /// <returns>结果</returns>
        bool AddRange(List<T> list);
        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="entity">列表</param>
        /// <returns>结果</returns>
        Task<bool> AddRangeAsync(List<T> list);

        /// <summary>
        /// 获取单个实体
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        T GetById(int id);


        /// <summary>
        /// 获取单个实体
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<T> GetByIdAsync(int id);

        /// <summary>
        /// 获取单个实体
        /// </summary>
        /// <param name="predicate">筛选</param>
        /// <returns>单个实体</returns>
        T GetOne(Expression<Func<T, bool>> predicate = null);

        /// <summary>
        /// 获取单个实体
        /// </summary>
        /// <param name="predicate">筛选</param>
        /// <returns>单个实体</returns>
        Task<T> GetOneAsync(Expression<Func<T, bool>> predicate = null);



        /// <summary>
        /// 获取所有数据
        /// </summary>
        /// <param name="predicate">筛选</param>
        /// <returns>数据集</returns>
        IQueryable<T> GetList(Expression<Func<T, bool>> predicate = null);

        /// <summary>
        /// 获取所有数据
        /// </summary>
        /// <param name="predicate">筛选</param>
        /// <returns>数据集</returns>
        Task<IQueryable<T>> GetListAsync(Expression<Func<T, bool>> predicate = null);


        /// <summary>
        /// 获取部分字段的所有数据
        /// </summary>
        /// <typeparam name="T1">返回数据集类型，可以是动态类型，类</typeparam>
        /// <param name="predicate">筛选</param>
        /// <param name="scalar">部分字段，例如:q=>new {id=q.id,name=q.name}、q=>new ClassA{id=q.id,name=q.name}</param>
        /// <returns>数据集</returns>
        IQueryable<T1> GetList<T1>(Expression<Func<T, bool>> predicate = null, Expression<Func<T, T1>> scalar = null);

        /// <summary>
        /// 获取部分字段的所有数据
        /// </summary>
        /// <typeparam name="T1">返回数据集类型，可以是动态类型，类</typeparam>
        /// <param name="predicate">筛选</param>
        /// <param name="scalar">部分字段，例如:q=>new {id=q.id,name=q.name}、q=>new ClassA{id=q.id,name=q.name}</param>
        /// <returns>数据集</returns>
        Task<IQueryable<T1>> GetListAsync<T1>(Expression<Func<T, bool>> predicate = null, Expression<Func<T, T1>> scalar = null);
        /// <summary>
        /// 软删除
        /// </summary>
        /// <param name="entity">实体</param>
        /// <returns></returns>
        bool Remove(int id);


        /// <summary>
        /// 软删除
        /// </summary>
        /// <param name="entity">实体</param>
        /// <returns></returns>
        Task<bool> RemoveAsync(int id);

        /// <summary>
        /// 硬删除
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        bool HardRemove(int id);
        /// <summary>
        /// 硬删除
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        Task<bool> HardRemoveAsync(int id);

        /// <summary>
        /// 该id是否存在
        /// </summary>
        /// <param name="id">编号</param>
        /// <returns>是否存在</returns>
        bool IsExist(int id);
        /// <summary>
        /// 该id是否存在
        /// </summary>
        /// <param name="id">编号</param>
        /// <returns>是否存在</returns>
        Task<bool> IsExistAsync(int id);

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="entity">实体</param>
        /// <returns></returns>
        bool Update(T entity);

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="entity">实体</param>
        /// <returns></returns>
        Task<bool> UpdateAsync(T entity);

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="list">实体列表</param>
        /// <returns></returns>
        bool UpdateRange(List<T> list);

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="list">实体列表</param>
        /// <returns></returns>
        Task<bool> UpdateRangeAsync(List<T> list);


    }
}
