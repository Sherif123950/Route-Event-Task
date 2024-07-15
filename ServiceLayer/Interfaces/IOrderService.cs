using DataAccessLayer.Entities;
using ServiceLayer.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Interfaces
{
	public interface IOrderService
	{
		public Task<OrderToReturnDto> AddOrder(Order order);
		public Task<IEnumerable<Order>> GetAllOrders();
		public Task<Order?> OrderById(int id);
		public Task<Order?> UpdateOrder(int orderId, string status);
	}
}
