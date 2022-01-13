using PWAProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PWADemoProject.Repository.IRepository
{
    public interface IProductRepository
    {
        void SaveProduct(Product foProduct, out int fiSuccess, out int fiProductId);
        List<ProductList> GetProduct(int? fiSortColumn, string fsSortOrder, int? fiPageNo, int? fiPageSize);
        Product GetProductDetail(int inProductId);
        void DeleteProduct(int inProductId,out int fiSuccess);
    }
}
