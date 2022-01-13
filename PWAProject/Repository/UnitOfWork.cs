
using PWADemoProject.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PWAProject.Data;

namespace PWADemoProject.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DatabaseContext moDatabaseContext;
        public UnitOfWork(DatabaseContext foDatabaseContext)
        {
            moDatabaseContext = foDatabaseContext;
            Products = new ProductRepository(moDatabaseContext);
        }
        public IProductRepository Products { get; private set; }
        public void Dispose()
        {
            moDatabaseContext.Dispose();
        }
        public void save()
        {
            moDatabaseContext.SaveChanges();
        }
    }
}
