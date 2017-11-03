using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using EA.Core.Data.Repository;
using EA.Core.Models;
using Microsoft.EntityFrameworkCore;
// ReSharper disable MemberCanBePrivate.Global

namespace EA.Data.Repository
{
    public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
    {
        protected readonly Microsoft.EntityFrameworkCore.DbContext Context;
        protected readonly DbSet<T> DbSet;

        public GenericRepository(Microsoft.EntityFrameworkCore.DbContext context)
        {
            this.Context = context;
            this.DbSet = context.Set<T>();
        }

        private IQueryable<T> GetQueryable(Expression<Func<T, bool>> filter = null,
            Expression<Func<T, object>> orderBy = null, string includeProperties = null, int? skip = null,
            int? take = null)
        {
            var query = DbSet.AsQueryable();
            
            if (includeProperties != null)
            {
                query = includeProperties.Split(new char[] {','}, StringSplitOptions.RemoveEmptyEntries).Aggregate(query, (current, property) => current.Include(property));
            }

            if (filter != null)
            {
                query = query.Where(filter);
            }
            
            if (orderBy != null)
            {
                query = query.OrderBy(orderBy);
            }

            if (skip != null)
            {
                query = query.Skip((int)skip);
            }

            if (take != null)
            {
                query = query.Take((int) take);
            }

            return query;
        }

        public virtual List<T> GetAll()
        {
            return DbSet.ToList();
        }

        public virtual List<T> GetBy(Expression<Func<T, bool>> filter = null, Expression<Func<T, object>> orderBy = null, string includeProperties = null, int? skip = null,
            int? take = null)
        {
            var query = GetQueryable(filter, orderBy, includeProperties, skip, take);
            return query.ToList();
        }

        public virtual T GetSingle(Expression<Func<T, bool>> filter = null, string includeProperties = null)
        {
            var query = GetQueryable(filter: filter, orderBy: null, includeProperties: includeProperties, skip: null,
                take: null);

            return query.FirstOrDefault();
        }

        public virtual T GetById(int id)
        {
            return DbSet.FirstOrDefault(e => e.Id == id);
        }

        public virtual Task<List<T>> GetAllAsync()
        {
            return DbSet.ToListAsync();
        }

        public virtual Task<List<T>> GetByAsync(Expression<Func<T, bool>> filter = null, Expression<Func<T, object>> orderBy = null, string includeProperties = null, int? skip = null,
            int? take = null)
        {
            var query = GetQueryable(filter, orderBy, includeProperties, skip, take);
            return query.ToListAsync();
        }

        public virtual Task<T> GetSingleAsync(Expression<Func<T, bool>> filter = null, string includeProperties = null)
        {
            var query = GetQueryable(filter: filter, includeProperties: includeProperties, orderBy: null, skip: null,
                take: null);

            return query.FirstOrDefaultAsync();
        }

        public virtual Task<T> GetByIdAsync(int id)
        {
            return DbSet.FirstOrDefaultAsync(e => e.Id == id);
        }
    }
}