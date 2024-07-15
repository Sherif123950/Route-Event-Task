using DataAccessLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Dtos
{
	public class OrderDto
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public DateTime OrderDate { get; set; }
		public decimal TotalAmount { get; set; }
		public int Customerid { get; set; }
		public List<OrderItemDto> OrderItems { get; set; } = new List<OrderItemDto>();
		public string PaymentMethod { get; set; }
		public string OrderStatus { get; set; }
	}
}
