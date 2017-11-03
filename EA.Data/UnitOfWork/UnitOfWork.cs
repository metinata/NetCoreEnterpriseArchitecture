using System;
using System.Collections;
using System.Threading.Tasks;
using EA.Core.Data.Repository;
using EA.Core.Models;
using EA.Data.Repository;
using Microsoft.EntityFrameworkCore;

namespace EA.Data.UnitofWork
{
    public class UnitOfWork
    {
        private readonly Microsoft.EntityFrameworkCore.DbContext _context;
        private readonly Hashtable _repos = new Hashtable();
        
        public UnitOfWork(Microsoft.EntityFrameworkCore.DbContext context)
        {
            this._context = context;
        }

        public IGenericRepository<T> Repository<T>() where T : BaseEntity
        {
            var type = typeof(T).Name;
            
            if (_repos.Contains(type))
            {
                return _repos[type] as IGenericRepository<T>;
            }

            _repos.Add(type, Activator.CreateInstance(typeof(GenericRepository<T>), _context));
            
            return _repos[type] as IGenericRepository<T>;
        }

        public int SaveChanges()
        {
            return _context.SaveChanges();
        }

        public Task<int> SaveChangesAsync()
        {
            return _context.SaveChangesAsync();
        } 
    }
}