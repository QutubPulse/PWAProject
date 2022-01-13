using PWADemoProject.Repository.IRepository;
using PWAProject.Data;
using PWAProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace PWADemoProject.Repository
{
    public class ProductRepository : IProductRepository
    {
        private readonly DatabaseContext moDatabaseContext;

        public ProductRepository(DatabaseContext foDatabaseContext)
        {
            moDatabaseContext = foDatabaseContext;
        }
        public void SaveProduct(Product foProduct, out int fiSuccess,out int fiProductId)
        {
            SqlParameter loSuccess = new SqlParameter("@inSuccess", SqlDbType.Int) { Direction = ParameterDirection.Output };
            SqlParameter liProductId = new SqlParameter("@inId", SqlDbType.Int) { Direction = ParameterDirection.Output };
            moDatabaseContext.Database.ExecuteSqlInterpolated($"EXEC saveProduct @inProductId={foProduct.inProductId},@stProductName={foProduct.stProductName},@dcPrice={foProduct.dcPrice},@stDescription={foProduct.stDescription},@dcDiscount={foProduct.dcDiscount},@inQuantity={foProduct.inQuantity}, @inSuccess={loSuccess} OUT,  @inId={liProductId} OUT");
            fiSuccess = Convert.ToInt32(loSuccess.Value);
            fiProductId= Convert.ToInt32(liProductId.Value);
        }
        public List<ProductList> GetProduct(int? fiSortColumn, string fsSortOrder, int? fiPageNo, int? fiPageSize)
        {
            return moDatabaseContext.Set<ProductList>().FromSqlInterpolated($"EXEC getProduct @inSortColumn={fiSortColumn},@stSortOrder={fsSortOrder}, @inPageNo={fiPageNo},@inPageSize={fiPageSize}").ToList();
        }
        public Product GetProductDetail(int inProductId)
        {
            return moDatabaseContext.Set<Product>().FromSqlInterpolated($"EXEC getProductDetails @inProductId={inProductId}").AsEnumerable().FirstOrDefault();
        }

        public void DeleteProduct(int fiProductId, out int fiSuccess)
        {
            SqlParameter loSuccess = new SqlParameter("@inSuccess",SqlDbType.Int) { Direction = ParameterDirection.Output };
            moDatabaseContext.Database.ExecuteSqlInterpolated($"EXEC deleteProduct @inProductId={fiProductId},@inSuccess = {loSuccess} OUT");
            fiSuccess = Convert.ToInt32(loSuccess.Value);
        }
    }
}
