using PWAProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PWADemoProject.Repository.IRepository
{
    public interface IProductRepository
    {
        void SaveProduct(Product foProduct, out int fiSuccess);
    }
}
