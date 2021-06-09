﻿using Microsoft.EntityFrameworkCore;
using Ordering.Application.Contracts.Persistence;
using Ordering.Domain.Common;
using Ordering.Infrastructure.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Ordering.Infrastructure.Repositories
{
    //Async Repository - Interfaces IAsyncRepository abstract interface
    public class RepositoryBase<T> : IAsyncRepository<T> where T : EntityBase
    {
        //Order context objects
        protected readonly OrderContext _dbContext;

        //Constructor
        public RepositoryBase(OrderContext dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        /// <summary>
        /// Get all items <typeparamref name="T"/> from Database Context
        /// </summary>
        /// <returns>IReadOnlyList <typeparamref name="T"/></returns>
        public async Task<IReadOnlyList<T>> GetAllAsync()
        {
            return await _dbContext.Set<T>().ToListAsync();
        }

        /// <summary>
        /// Get all items <typeparamref name="T"/> from Database Context<para />
        /// Where <typeparamref name="predicate"/>
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns>IReadOnlyList <typeparamref name="T"/></returns>
        public async Task<IReadOnlyList<T>> GetAsync(Expression<Func<T, bool>> predicate)
        {
            return await _dbContext.Set<T>().Where(predicate).ToListAsync();
        }

        /// <summary>
        /// Get all items <typeparamref name="T"/> from Database Context<para />
        /// Where <typeparamref name="predicate"/>
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="orderBy"></param>
        /// <param name="includeString"></param>
        /// <param name="disableTracking"></param>
        /// <returns>IReadOnlyList <typeparamref name="T"/></returns>
        public async Task<IReadOnlyList<T>> GetAsync(Expression<Func<T, bool>> predicate = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, string includeString = null, bool disableTracking = true)
        {
            //Create queryable object from dbcontext with type T
            IQueryable<T> query = _dbContext.Set<T>();
            if (disableTracking) query = query.AsNoTracking();

            if (!string.IsNullOrWhiteSpace(includeString)) query = query.Include(includeString);

            if (predicate != null) query = query.Where(predicate);

            if (orderBy != null)
                return await orderBy(query).ToListAsync(); //Return ordered list
            return await query.ToListAsync(); //Return unordered list
        }

        /// <summary>
        /// Get all items <typeparamref name="T"/> from Database Context<para />
        /// Where <typeparamref name="predicate"/>
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="orderBy"></param>
        /// <param name="includes"></param>
        /// <param name="disableTracking"></param>
        /// <returns>IReadOnlyList <typeparamref name="T"/></returns>
        public async Task<IReadOnlyList<T>> GetAsync(Expression<Func<T, bool>> predicate = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, List<Expression<Func<T, object>>> includes = null, bool disableTracking = true)
        {
            //Create queryable object from dbcontext with type T
            IQueryable<T> query = _dbContext.Set<T>();
            if (disableTracking) query = query.AsNoTracking();

            if (includes != null) query = includes.Aggregate(query, (current, include) => current.Include(include));

            if (predicate != null) query = query.Where(predicate);

            if (orderBy != null)
                return await orderBy(query).ToListAsync(); //Return ordered list
            return await query.ToListAsync(); //Return unordered list
        }

        /// <summary>
        /// Async get <typeparamref name="T"/> By <typeparamref name="id"/>
        /// </summary>
        /// <param name="id"></param>
        /// <returns><typeparamref name="T"/></returns>
        public async Task<T> GetByIdAsync(int id)
        {
            return await _dbContext.Set<T>().FindAsync(id);
        }

        /// <summary>
        /// Async create new <typeparamref name="entity"/>
        /// </summary>
        /// <param name="entity"></param>
        /// <returns><typeparamref name="entity"/></returns>
        public async Task<T> AddAsync(T entity)
        {
            _dbContext.Set<T>().Add(entity);
            await _dbContext.SaveChangesAsync();
            return entity;
        }

        /// <summary>
        /// Async update <typeparamref name="T"/>
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public async Task UpdateAsync(T entity)
        {
            _dbContext.Entry(entity).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
        }

        /// <summary>
        /// Async delete <typeparamref name="entity"/>
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public async Task DeleteAsync(T entity)
        {
            _dbContext.Set<T>().Remove(entity);
            await _dbContext.SaveChangesAsync();
        }
    }
}
