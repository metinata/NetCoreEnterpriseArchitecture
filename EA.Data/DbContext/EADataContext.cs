using System;
using EA.Core.Models;
using EA.Data.Repository;
using EA.Data.UnitofWork;
using Microsoft.EntityFrameworkCore;

namespace EA.Data.DbContext
{
    // ReSharper disable once InconsistentNaming
    public class EADataContext : Microsoft.EntityFrameworkCore.DbContext
    {
        public EADataContext(DbContextOptions<EADataContext> options) : base(options)
        {
            
        }
        
        public DbSet<User> User { get; set; }
        public DbSet<Product> Product { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            
        }
    }
}