using DataAccessLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.Interfaces
{
	public interface IOrderRepository
	{
		public Task<Order?> AddOrder(Order order);
		public Task<IEnumerable<Order>> GetAllOrders();
		public Task<Order?> OrderById(int id);
		public Task<Order?> UpdateOrder(int orderId, string status);
	}
}
