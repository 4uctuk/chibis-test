using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using ChibisTest.DataAccess.Entities;
using ChibisTest.Features.Common;
using ChibisTest.Features.Common.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace ChibisTest.Features.Cart
{
    [Route("cart")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly ICartService _cartService;

        public CartController(ICartService cartService)
        {
            _cartService = cartService;
        }

        [HttpGet]
        [ProducesResponseType(typeof(ItemsPaged<DataAccess.Entities.Cart>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetCarts(int limit = 50, int offset = 0)
        {
            var carts = await _cartService.GetAllCartsPaged(limit, offset);
            return Ok(carts);
        }
        
        [HttpGet("{cartId}")]
        [ProducesResponseType(typeof(List<CartItem>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(Guid), (int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> GetCartItems(Guid cartId)
        {
            try
            {
                var cartItems = await _cartService.GetCartItems(cartId);
                return Ok(cartItems);
            }
            catch (EntityNotFoundException)
            {
                return NotFound(cartId);
            }
          
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddProductToCartDto productCartDto)
        {
            try
            {
                await _cartService.AddCartItem(productCartDto);
                return Ok();
            }
            catch (ArgumentNullException e)
            {
                return BadRequest(e.Message);
            }
            catch (InvalidOperationException e)
            {
                return BadRequest(e.Message);
            }
        }


        [HttpDelete]
        public async Task<IActionResult> Delete(DeleteProductFromCartDto productCartDto)
        {
            try
            {
                await _cartService.DeleteCartItem(productCartDto);
                return Ok();
            }
            catch (ArgumentNullException e)
            {
                return BadRequest(e.Message);
            }
            catch (InvalidOperationException e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
