using System.Collections.Generic;
using System.Threading.Tasks;
using ChibisTest.Features.Common;
using ChibisTest.Features.DataAccess.Entities;

namespace ChibisTest.Features.Products
{
    public interface IProductsService
    {
        Task<ItemsPaged<Product>> GetAllProductsPaged(int limit, int offset);

        Task<Product> GetProductById(int id);

        Task<int> CreateProduct(CreateAndUpdateProductDto productDto);
        Task UpdateProduct(int id, CreateAndUpdateProductDto productDto);
        Task DeleteProduct(int id);
    }
}
