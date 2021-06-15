using Ordering.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Ordering.Application.Contracts.Persistence
{
    //Abstract functions for the repository
    public interface IAsyncRepository<T> where T : EntityBase
    {
        /// <summary>
        /// Get all items <typeparamref name="T"/> from Database Context
        /// </summary>
        /// <returns>IReadOnlyList <typeparamref name="T"/></returns>
        Task<IReadOnlyList<T>> GetAllAsync();

        /// <summary>
        /// Get all items <typeparamref name="T"/> from Database Context<para />
        /// Where <typeparamref name="predicate"/>
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns>IReadOnlyList <typeparamref name="T"/></returns>
        Task<IReadOnlyList<T>> GetAsync(Expression<Func<T, bool>> predicate);

        /// <summary>
        /// Get all items <typeparamref name="T"/> from Database Context<para />
        /// Where <typeparamref name="predicate"/>
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="orderBy"></param>
        /// <param name="includeString"></param>
        /// <param name="disableTracking"></param>
        /// <returns>IReadOnlyList <typeparamref name="T"/></returns>
        Task<IReadOnlyList<T>> GetAsync(Expression<Func<T, bool>> predicate = null,
                                                                  Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
                                                                  string includeString = null,
                                                                  bool disableTracking = true);

        /// <summary>
        /// Get all items <typeparamref name="T"/> from Database Context<para />
        /// Where <typeparamref name="predicate"/>
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="orderBy"></param>
        /// <param name="includes"></param>
        /// <param name="disableTracking"></param>
        /// <returns>IReadOnlyList <typeparamref name="T"/></returns>
        Task<IReadOnlyList<T>> GetAsync(Expression<Func<T, bool>> predicate = null,
                                                                  Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
                                                                  List<Expression<Func<T, object>>> includes = null,
                                                                  bool disableTracking = true);
        /// <summary>
        /// Async get <typeparamref name="T"/> By <typeparamref name="id"/>
        /// </summary>
        /// <param name="id"></param>
        /// <returns><typeparamref name="T"/></returns>
        Task<T> GetByIdAsync(int id);

        /// <summary>
        /// Async get <typeparamref name="T"/> By <typeparamref name="ids"/>
        /// </summary>
        /// <param name="ids"></param>
        /// <returns><typeparamref name="T"/></returns>
        Task<List<T>> GetByIdListAsync(List<int> ids);

        /// <summary>
        /// Async create new <typeparamref name="entity"/>
        /// </summary>
        /// <param name="entity"></param>
        /// <returns><typeparamref name="entity"/></returns>
        Task<T> AddAsync(T entity);

        /// <summary>
        /// Async update <typeparamref name="T"/>
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        Task UpdateAsync(T entity);

        /// <summary>
        /// Async delete <typeparamref name="entity"/>
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        Task DeleteAsync(T entity);

        /// <summary>
        /// Async delete many <typeparamref name="entites"/>
        /// </summary>
        /// <param name="entities"></param>
        /// <returns></returns>
        Task DeleteManyAsync(List<T> entities);
    }
}
