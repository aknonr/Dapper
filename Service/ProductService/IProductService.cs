using Entities;
using Entities.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.ProductService
{
    public interface IProductService
    {
        Task<Product> GetProductByIdAsync(int id);

        Task<List<Product>> GetAllProductsAsync(DateTime date, bool isTransferred);

        Task<ProductColor> GetProductColorByIdAsync(int id);

        Task<List<ProductColor>> GetAllProductColorsAsync(DateTime date, bool isTransferred);



    }
}
