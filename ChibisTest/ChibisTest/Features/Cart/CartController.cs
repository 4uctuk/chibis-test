using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using ChibisTest.Features.Common;
using ChibisTest.Features.Common.Exceptions;
using ChibisTest.Features.DataAccess.Entities;
using Microsoft.AspNetCore.Mvc;

namespace ChibisTest.Features.Cart
{
    [Route("cart")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly ICartCrudService _cartCrudService;

        public CartController(ICartCrudService cartCrudService)
        {
            _cartCrudService = cartCrudService;
        }

        /// <summary>
        /// Return all carts
        /// </summary>
        /// <param name="limit">limit</param>
        /// <param name="offset">offset</param>
        /// <returns>paged carts</returns>
        [HttpGet]
        [ProducesResponseType(typeof(ItemsPaged<DataAccess.Entities.Cart>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetCarts(int limit = 50, int offset = 0)
        {
            var carts = await _cartCrudService.GetAllCartsPaged(limit, offset);
            return Ok(carts);
        }
        
        /// <summary>
        /// Return all cart items from cart
        /// </summary>
        /// <param name="cartId">cart id</param>
        /// <returns></returns>
        [HttpGet("{cartId}")]
        [ProducesResponseType(typeof(List<CartItem>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(Guid), (int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> GetCartItems(Guid cartId)
        {
            try
            {
                var cartItems = await _cartCrudService.GetCartItems(cartId);
                return Ok(cartItems);
            }
            catch (EntityNotFoundException)
            {
                return NotFound(cartId);
            }
          
        }

        /// <summary>
        /// Add product to cart (create new cart if cartId not exist)
        /// </summary>
        /// <param name="productCartDto"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Add(AddProductToCartDto productCartDto)
        {
            try
            {
                var cartId = await _cartCrudService.AddCartItem(productCartDto);
                return Ok(cartId);
            }
            catch (ArgumentNullException e)
            {
                return BadRequest(e.Message);
            }
            catch (EntityNotFoundException e)
            {
                return BadRequest(e.Message);
            }
        }

        /// <summary>
        /// Delete product from cart
        /// </summary>
        /// <param name="productCartDto"></param>
        /// <returns></returns>
        [HttpDelete]
        public async Task<IActionResult> Delete(DeleteProductFromCartDto productCartDto)
        {
            try
            {
                await _cartCrudService.DeleteCartItem(productCartDto);
                return Ok();
            }
            catch (ArgumentNullException e)
            {
                return BadRequest(e.Message);
            }
            catch (EntityNotFoundException e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
