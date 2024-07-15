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
	public class OrderRepository : IOrderRepository
	{
		private readonly OrderManagementDbContext _dbContext;

		public OrderRepository(OrderManagementDbContext dbContext)
		{
			this._dbContext = dbContext;
		}
		public async Task<Order?> AddOrder(Order order)
		{
			await _dbContext.Orders.AddAsync(order);
			var result = await _dbContext.SaveChangesAsync();
			if (result == 0)
				return null;
			return order;
		}

		public async Task<IEnumerable<Order>> GetAllOrders()
		{
			return await _dbContext.Orders.Include(o=>o.OrderItems).ToListAsync();
		}

		public async Task<Order?> OrderById(int id)
		{
			return await _dbContext.Orders.Include(os => os.OrderItems).SingleOrDefaultAsync(o => o.Id == id);
		}

		public async Task<Order?> UpdateOrder(int orderId, string status)
		{
			var order = await _dbContext.Orders.SingleOrDefaultAsync(o => o.Id == orderId);
			if (order == null) { return null; }
			order.OrderStatus = status;
			await _dbContext.SaveChangesAsync();
			return order;
		}
	}
}
