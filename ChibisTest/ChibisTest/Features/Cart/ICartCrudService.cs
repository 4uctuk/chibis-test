using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ChibisTest.Features.Common;
using ChibisTest.Features.DataAccess.Entities;

namespace ChibisTest.Features.Cart
{
    public interface ICartCrudService
    {
        /// <summary>
        /// Return all carts paged
        /// </summary>
        /// <param name="limit"></param>
        /// <param name="offset"></param>
        /// <returns></returns>
        Task<ItemsPaged<DataAccess.Entities.Cart>> GetAllCartsPaged(int limit, int offset);

        /// <summary>
        /// Return cart items
        /// </summary>
        /// <param name="cartId">Cart id</param>
        /// <returns></returns>
        Task<List<CartItem>> GetCartItems(Guid cartId);

        /// <summary>
        /// Add item to cart
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        Task<Guid> AddCartItem(AddProductToCartDto item);

        /// <summary>
        /// Delete item from cart
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        Task DeleteCartItem(DeleteProductFromCartDto item);
    }
}
