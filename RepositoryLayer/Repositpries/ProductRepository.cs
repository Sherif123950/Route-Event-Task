using DataAccessLayer.Data.Contexts;
using DataAccessLayer.Entities;
using Microsoft.EntityFrameworkCore;
using RepositoryLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.Repositpries
{
	public class ProductRepository : IProductRepository
	{
		private readonly OrderManagementDbContext _dbContext;

		public ProductRepository(OrderManagementDbContext dbContext)
		{
			this._dbContext = dbContext;
		}
		public async Task<Product?> AddProduct(Product Product)
		{
			await _dbContext.Products.AddAsync(Product);
			var res = await _dbContext.SaveChangesAsync();
			if (res == 0) return null;
            return Product;
		}

		public async Task<IEnumerable<Product>> GetAllProducts()
		{
			return await _dbContext.Products.ToListAsync();
		}

		public async Task<Product?> ProductById(int id)
		{
			return await _dbContext.Products.SingleOrDefaultAsync(p => p.Id == id);
		}

		public async Task<Product?> UpdateProduct(Product product)
		{
			var existingProduct = await _dbContext.Products.FindAsync(product.Id);
			if (existingProduct == null)
			{
				return null;
			}

			existingProduct.Name = product.Name;
			existingProduct.Price = product.Price;
			existingProduct.Stock = product.Stock;

			_dbContext.Products.Update(existingProduct);
			await _dbContext.SaveChangesAsync();

			return existingProduct;
		}
	}
}
