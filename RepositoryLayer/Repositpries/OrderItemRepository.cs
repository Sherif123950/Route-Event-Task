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
	public class OrderItemRepository :IOrderItemRepository
	{
		private readonly OrderManagementDbContext _dbContext;

		public OrderItemRepository(OrderManagementDbContext dbContext)
		{
			_dbContext = dbContext;
		}

		public async Task<OrderItem?> AddOrderItem(OrderItem orderItem)
		{
			await _dbContext.OrderItems.AddAsync(orderItem);
			var result = await _dbContext.SaveChangesAsync();
			if (result == 0)
				return null;
			return orderItem;
		}

		public async Task<IEnumerable<OrderItem>> GetOrderItemsByOrderId(int orderId)
		{
			return await _dbContext.OrderItems
				.Where(oi => oi.OrderId == orderId)
				.ToListAsync();
		}

		public async Task<OrderItem?> UpdateOrderItem(OrderItem orderItem)
		{
			_dbContext.OrderItems.Update(orderItem);
			var result = await _dbContext.SaveChangesAsync();
			if (result == 0)
				return null;
			return orderItem;
		}
	}
}
