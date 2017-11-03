using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using EA.Core.Models;

namespace EA.Core.Data.Repository
{
    public interface IRepositoryAsync<TEntity> where TEntity : BaseEntity
    {
        Task<List<TEntity>> GetAllAsync();

        Task<List<TEntity>> GetByAsync(
            Expression<Func<TEntity, bool>> filter = null,
            Expression<Func<TEntity, object>> orderBy = null,
            string includeProperties = null,
            int? skip = null,
            int? take = null
        );

        Task<TEntity> GetSingleAsync(
            Expression<Func<TEntity, bool>> filter = null,
            string includeProperties = null
        );

        Task<TEntity> GetByIdAsync(int id);
    }
}