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
	public class CustomerRepository : ICustomerRepository
	{
		private readonly OrderManagementDbContext _dbContext;

		public CustomerRepository(OrderManagementDbContext dbContext)
		{
			this._dbContext = dbContext;
		}
		public async Task<Customer?> AddCustomer(Customer customer)
		{
			await _dbContext.Customers.AddAsync(customer);
			var res = await _dbContext.SaveChangesAsync();
			if (res == 0) return null;
			return customer;
		}

		public async Task<IEnumerable<Order>> GetCustomerOrders(int customerId)
		{
			return await _dbContext.Orders.Where(o => o.Customerid == customerId).Include(o => o.OrderItems).ToListAsync();
		}

		public async Task<Customer?> GetCustomerById(int id)
		{
			return await _dbContext.Customers.SingleOrDefaultAsync(c => c.Id == id);
		}

	}
}
