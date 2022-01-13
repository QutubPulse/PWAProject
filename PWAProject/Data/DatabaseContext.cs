using Microsoft.EntityFrameworkCore;
using PWAProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PWAProject.Data
{
    public class DatabaseContext: DbContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
        {
        }
        public DbSet<Product> Product { get; set; }
        public DbSet<ProductList> ProductList { get; set; }
        public DbSet<PwaUsers> PwaUsers { get; set; }
        public DbSet<UserSessionDetail> UserSessionDetail { get; set; }

        protected override void OnModelCreating(ModelBuilder foModelBuilder)
        {
            foModelBuilder.Entity<Product>().HasNoKey();
            foModelBuilder.Entity<ProductList>().HasNoKey();
            foModelBuilder.Entity<PwaUsers>().HasNoKey();
            foModelBuilder.Entity<UserSessionDetail>().HasNoKey();
            base.OnModelCreating(foModelBuilder);
        }
    }
}
