using DataAccessLayer.Data.Contexts;
using DataAccessLayer.Entities;
using Microsoft.EntityFrameworkCore;
using RepositoryLayer.Interfaces;
using ServiceLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Services
{
	public class ProductService : IProductService
	{
		private readonly IProductRepository _ProductRepository;

		public ProductService(IProductRepository ProductRepository)
		{
			this._ProductRepository = ProductRepository;
		}
		public async Task<Product?> AddProduct(Product Product)
		{
			return await _ProductRepository.AddProduct(Product);
		}

		public async Task<IEnumerable<Product>> GetAllProducts()
		{
			return await _ProductRepository.GetAllProducts();
		}

		public async Task<Product?> ProductById(int id)
		{
			return await _ProductRepository.ProductById(id);
		}

		public async Task<Product?> UpdateProduct(Product product)
		{
			return await _ProductRepository.UpdateProduct(product);
		}
	}
}
