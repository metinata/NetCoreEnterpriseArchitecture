using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using EA.Core.Models;

namespace EA.Core.Data.Repository
{
    public interface IGenericRepository<TEntity> : IRepository<TEntity>, IRepositoryAsync<TEntity>
        where TEntity : BaseEntity
    {
    }
}