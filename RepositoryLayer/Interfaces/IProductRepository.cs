using DataAccessLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.Interfaces
{
	public interface IProductRepository
	{
		public Task<Product?> AddProduct(Product product);
		public Task<IEnumerable<Product>> GetAllProducts();
		public Task<Product?> ProductById(int id);
		public Task<Product?> UpdateProduct(Product product);

	}
}
