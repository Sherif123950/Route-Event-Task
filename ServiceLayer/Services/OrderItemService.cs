using DataAccessLayer.Entities;
using RepositoryLayer.Interfaces;
using ServiceLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Services
{
	public class OrderItemService:IOrderItemService
	{
		private readonly IOrderItemRepository _orderItemRepository;

		public OrderItemService(IOrderItemRepository orderItemRepository)
		{
			_orderItemRepository = orderItemRepository;
		}

		public async Task<OrderItem?> AddOrderItem(OrderItem orderItem)
		{
			return await _orderItemRepository.AddOrderItem(orderItem);
		}

		public async Task<IEnumerable<OrderItem>> GetOrderItemsByOrderId(int orderId)
		{
			return await _orderItemRepository.GetOrderItemsByOrderId(orderId);
		}

		public async Task<OrderItem?> UpdateOrderItem(OrderItem orderItem)
		{
			return await _orderItemRepository.UpdateOrderItem(orderItem);
		}
	}
}
