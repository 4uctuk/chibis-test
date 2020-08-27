using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ChibisTest.Features.Common;
using ChibisTest.Features.Common.Exceptions;
using ChibisTest.Features.DataAccess;
using ChibisTest.Features.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;

namespace ChibisTest.Features.Cart
{
    public class CartCrudService : ICartCrudService
    {
        private readonly ApplicationDbContext _context;

        private IQueryable<DataAccess.Entities.Cart> SetWithRelatedEntities
        {
            get
            {
                return _context.Set<DataAccess.Entities.Cart>()
                    .Include("CartItems.Product");
            }
        }

        private IQueryable<DataAccess.Entities.Cart> SetWithRelatedEntitiesAsNoTracking
        {
            get
            {
                return SetWithRelatedEntities.AsNoTracking();
            }
        }

		public CartCrudService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<ItemsPaged<DataAccess.Entities.Cart>> GetAllCartsPaged(int limit, int offset)
        {
            var totalCount = await _context.Carts.CountAsync();
            var items = await SetWithRelatedEntitiesAsNoTracking.Skip(offset).Take(limit).ToListAsync();
            return new ItemsPaged<DataAccess.Entities.Cart>()
            {
                Items = items,
                TotalCount = totalCount
            };
        }

        public async Task<List<CartItem>> GetCartItems(Guid cartId)
        {
            var cart = await GetCartById(cartId);
            return cart != null ? cart.CartItems.ToList() : new List<CartItem>();
        }

        public async Task<Guid> AddCartItem(AddProductToCartDto item)
        {
            if (item == null)
            {
                throw new ArgumentNullException(nameof(item));
            }
            var product = await _context.Products.FirstOrDefaultAsync(c => c.Id == item.ProductId);
            
            if (product == null)
            {
                throw new EntityNotFoundException($"Product with id:{item.ProductId} doesn't exist");
            }

            var cart = await GetCartById(item.CartId);
            if (cart == null)
            {
                cart = await CreateNewCart(item);
            }
            else
            {
                await AddItemToExistingCart(item, cart);
            }

            await _context.SaveChangesAsync();

            return cart.CartId;
        }

        private async Task AddItemToExistingCart(AddProductToCartDto item, DataAccess.Entities.Cart cart)
        {
            var cartItem = cart.CartItems.FirstOrDefault(c => c.ProductId == item.ProductId);
            if (cartItem != null)
            {
                cartItem.Quantity++;
                _context.CartItems.Update(cartItem);
            }
            else
            {
                cartItem = new CartItem()
                {
                    CartId = cart.Id,
                    ProductId = item.ProductId,
                    ForBonusPoints = item.ForBonusPoints,
                    Quantity = 1
                };
                await _context.CartItems.AddAsync(cartItem);
            }
        }

        private async Task<DataAccess.Entities.Cart> CreateNewCart(AddProductToCartDto item)
        {
            var cart = new DataAccess.Entities.Cart()
            {
                CartId = item.CartId
            };

            var result = await _context.Carts.AddAsync(cart);
            await _context.SaveChangesAsync();

            var cartItem = new CartItem()
            {
                CartId = result.Entity.Id,
                ProductId = item.ProductId,
                ForBonusPoints = item.ForBonusPoints,
                Quantity = 1
            };

            await _context.CartItems.AddAsync(cartItem);

            return cart;
        }
        
        public async Task DeleteCartItem(DeleteProductFromCartDto item)
        {
            if (item == null)
            {
                throw new ArgumentNullException(nameof(item));
            }

            var cart = await GetCartById(item.CartId);
            if (cart == null)
            {
                throw new EntityNotFoundException($"Cart with id:{item.CartId} doesn't exist");
            }

            var cartItem = cart.CartItems.FirstOrDefault(c => c.ProductId == item.ProductId);
            if (cartItem == null)
            {
                throw new EntityNotFoundException($"Cart item with product id:{item.ProductId} doesn't exist");
            }

            cartItem.Quantity--;
            if (cartItem.Quantity == 0)
            {
                _context.CartItems.Remove(cartItem);
            }
            else
            {
                _context.CartItems.Update(cartItem);
            }

            await _context.SaveChangesAsync();
        }

        private async Task<DataAccess.Entities.Cart> GetCartById(Guid cartId)
        {
            return await SetWithRelatedEntities.FirstOrDefaultAsync(c => c.CartId == cartId);
        }
    }
}
