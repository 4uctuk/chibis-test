using System.Threading.Tasks;
using ChibisTest.Features.Common;
using ChibisTest.Features.DataAccess.Entities;

namespace ChibisTest.Features.Products
{
    public interface IProductsService
    {
        /// <summary>
        /// Get products paged
        /// </summary>
        /// <param name="limit">limit</param>
        /// <param name="offset">offset</param>
        /// <returns></returns>
        Task<ItemsPaged<Product>> GetAllProductsPaged(int limit, int offset);

        /// <summary>
        /// Get product by Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<Product> GetProductById(int id);

        /// <summary>
        /// Create new product
        /// </summary>
        /// <param name="productDto"></param>
        /// <returns></returns>
        Task<int> CreateProduct(CreateAndUpdateProductDto productDto);

        /// <summary>
        /// Update product
        /// </summary>
        /// <param name="id"></param>
        /// <param name="productDto"></param>
        /// <returns></returns>
        Task UpdateProduct(int id, CreateAndUpdateProductDto productDto);

        /// <summary>
        /// Delete product
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task DeleteProduct(int id);
    }
}
