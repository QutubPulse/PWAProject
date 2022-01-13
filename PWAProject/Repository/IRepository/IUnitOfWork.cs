using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PWADemoProject.Repository.IRepository
{
    public interface IUnitOfWork : IDisposable
    {
        IProductRepository Products { get; }
        void save();
    }
}
