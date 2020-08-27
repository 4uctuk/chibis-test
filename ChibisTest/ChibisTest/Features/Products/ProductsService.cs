using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ChibisTest.DataAccess;
using ChibisTest.DataAccess.Entities;
using ChibisTest.Features.Common;
using ChibisTest.Features.Common.Exceptions;
using ChibisTest.Features.Products.Dto;
using Microsoft.EntityFrameworkCore;

namespace ChibisTest.Features.Products
{
    public class ProductsService : IProductsService
    {
        private readonly ApplicationDbContext _context;
        
        public ProductsService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<ItemsPaged<Product>> GetAllProductsPaged(int limit, int offset)
        {
            var totalCount = await _context.Products.CountAsync();
            var products = await _context.Products.Skip(offset).Take(limit).ToListAsync();

            return new ItemsPaged<Product>()
            {
                TotalCount = totalCount,
                Items = products
            };
        }

        public async Task<Product> GetProductById(int id)
        {
            var item = await _context.Products.FirstOrDefaultAsync(c => c.Id == id);
            if(item == null)
                throw new EntityNotFoundException();
            return item;
        }

        public async Task<int> CreateProduct(CreateAndUpdateProductDto productDto)
        {
            if (productDto == null)
                throw new ArgumentNullException(nameof(productDto));

            var product = new Product
            {
                Cost = productDto.Cost,
                Name = productDto.Name
            };

            var result = await _context.Products.AddAsync(product);
            await _context.SaveChangesAsync();
            return result.Entity.Id;
        }

        public async Task UpdateProduct(int id, CreateAndUpdateProductDto productDto)
        {
            if (productDto == null)
                throw new ArgumentNullException(nameof(productDto));

            var product = await GetProductById(id);

            if (product.Cost != productDto.Cost)
                product.Cost = productDto.Cost;

            if (!string.IsNullOrWhiteSpace(productDto.Name) && product.Name != productDto.Name)
            {
                product.Name = productDto.Name;
            }

            _context.Products.Update(product);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteProduct(int id)
        {
            var product = await GetProductById(id);
            _context.Products.Remove(product); 
            await _context.SaveChangesAsync();
        }
    }
}
