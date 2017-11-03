using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using EA.Core.Models;

namespace EA.Core.Data.Repository
{
    public interface IRepository<TEntity> where TEntity : BaseEntity
    {
        List<TEntity> GetAll();

        List<TEntity> GetBy(
            Expression<Func<TEntity, bool>> filter = null,
            Expression<Func<TEntity, object>> orderBy = null,
            string includeProperties = null,
            int? skip = null,
            int? take = null
        );

        TEntity GetSingle(
            Expression<Func<TEntity, bool>> filter = null,
            string includeProperties = null
        );

        TEntity GetById(int id);
    }
}