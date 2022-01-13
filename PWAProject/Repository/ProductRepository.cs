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

        public void SaveProduct(Product foProduct, out int fiSuccess)
        {
            SqlParameter loSuccess = new SqlParameter("@inSuccess", SqlDbType.Int) { Direction = ParameterDirection.Output };
            moDatabaseContext.Database.ExecuteSqlInterpolated($"EXEC saveProduct @inId={foProduct.inProductId},@stFirstName={foProduct.stProductName},@dcPrice={foProduct.dcPrice},@stDescription={foProduct.stDescription},@stDiscount={foProduct.stDiscount},@inQuantity={foProduct.inQuantity}, @inSuccess={loSuccess} OUT");
            fiSuccess = Convert.ToInt32(loSuccess.Value);
        }
    }
}
