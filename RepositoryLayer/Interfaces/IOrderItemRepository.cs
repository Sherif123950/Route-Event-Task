using DataAccessLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.Interfaces
{
	public interface IOrderItemRepository
	{
		Task<OrderItem?> AddOrderItem(OrderItem orderItem);
		Task<IEnumerable<OrderItem>> GetOrderItemsByOrderId(int orderId);
		Task<OrderItem?> UpdateOrderItem(OrderItem orderItem);
	}
}
