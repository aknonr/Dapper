using Dapper;
using Entities;
using Entities.Model;
using Handler;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;


namespace Services.ProductService
{
    public class ProductService : IProductService
    {

        public SqlConnection sqlConnection { get => new SqlConnection("Data Source=DATABASE IP ADRESS;Initial Catalog=DB NAME;Integrated Security=False; Info=False;"); set { } }

        public ProductService() { } // Constructor Metodu

        public async Task<Product> GetProductByIdAsync(int id)
        {
            var query = "SELECT * FROM Products WHERE Id = @Id";
            return await DbHandler.QuerySingleAsync<Product>(query, new { Id = id });
        }

        public async Task<List<Product>> GetAllProductsAsync(DateTime date, bool isTransferred)
        {
            var isTransferredValue = isTransferred ? 1 : 0;
            var startDate = date.Date;
            var query = "SELECT * FROM Products WHERE CAST(CONVERT(VARCHAR, TransferDate, 110) AS DATE) = @Date AND IsTransferred = @IsTransferred";
            var products = await DbHandler.QueryAsync<Product>(query, new { Date = startDate, IsTransferred = isTransferredValue });
            return products.ToList();
        }

        public async Task<ProductColor> GetProductColorByIdAsync(int id)
        {
            var query = "SELECT * FROM ProductColors WHERE Id = @Id";
            return await DbHandler.QuerySingleAsync<ProductColor>(query, new { Id = id });
        }

        public async Task<List<ProductColor>> GetAllProductColorsAsync(DateTime date, bool isTransferred)
        {
            var isTransferredValue = isTransferred ? 1 : 0;
            var startDate = date.Date;
            var query = "SELECT * FROM ProductColors WHERE CAST(CONVERT(VARCHAR, TransferDate, 110) AS DATE) = @Date AND IsTransferred = @IsTransferred";
            var productColors = await DbHandler.QueryAsync<ProductColor>(query, new { Date = startDate, IsTransferred = isTransferredValue });
            return productColors.ToList();
        }
    }
}
