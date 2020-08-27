using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using ChibisTest.Features.Common;
using ChibisTest.Features.Common.Exceptions;
using ChibisTest.Features.DataAccess.Entities;
using Microsoft.AspNetCore.Mvc;

namespace ChibisTest.Features.Products
{
    [ApiController]
    [Route("products")]
    public class ProductsController : ControllerBase
    {
        private readonly IProductsService _service;

        public ProductsController(IProductsService service)
        {
            _service = service;
        }

        /// <summary>
        /// Get all products in database
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(typeof(ItemsPaged<Product>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Get(int limit, int offset)
        {
            var result = await _service.GetAllProductsPaged(limit, offset);
            return Ok(result);
        }

        /// <summary>
        /// Get product by Id
        /// </summary>
        /// <param name="id">Id</param>
        /// <returns>Product entity</returns>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(Product), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(int), (int)HttpStatusCode.NotFound)]

        public async Task<IActionResult> Get(int id)
        {
            try
            {
                var result = await _service.GetProductById(id);
                return Ok(result);
            }
            catch (EntityNotFoundException)
            {
                return NotFound(id);
            }
        }

        /// <summary>
        /// Create new product item
        /// </summary>
        /// <param name="product">Product dto for creation</param>
        /// <returns>Id of created product</returns>
        [HttpPost]
        [ProducesResponseType(typeof(int), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Post([FromBody] CreateAndUpdateProductDto product)
        {
            var id = await _service.CreateProduct(product);
            return Ok(id);
        }

        /// <summary>
        /// Update product
        /// </summary>
        /// <param name="id">Product id</param>
        /// <param name="product">Product dto for changes</param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(int), (int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> Put(int id, [FromBody] CreateAndUpdateProductDto product)
        {
            try
            {
                await _service.UpdateProduct(id, product);
                return Ok();
            }
            catch (EntityNotFoundException)
            {
                return NotFound(id);
            }
        }

        /// <summary>
        /// Delete product by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _service.DeleteProduct(id);
                return Ok();
            }
            catch (EntityNotFoundException)
            {
                return NotFound(id);
            }
        }
    }
}
