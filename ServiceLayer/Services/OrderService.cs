using DataAccessLayer.Data.Contexts;
using DataAccessLayer.Entities;
using Microsoft.EntityFrameworkCore;
using RepositoryLayer.Interfaces;
using ServiceLayer.Dtos;
using ServiceLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Services
{
	public class OrderService : IOrderService
	{
		private readonly IOrderItemService _orderItemService;
		private readonly IEmailService _emailService;
		private readonly IOrderRepository _orderRepository;
		private readonly IProductService _productService;
		private readonly IinvoiceService _invoiceService;
		private readonly ICustomerService _customerService;

		public OrderService(IOrderItemService orderItemService,IEmailService emailService, IOrderRepository orderRepository, IProductService productService, IinvoiceService invoiceService, ICustomerService customerService)
		{
			this._orderItemService = orderItemService;
			this._emailService = emailService;
			this._orderRepository = orderRepository;
			this._productService = productService;
			this._invoiceService = invoiceService;
			this._customerService = customerService;
		}
		public async Task<OrderToReturnDto> AddOrder(Order order)
		{
			var orderToReturnDto = new OrderToReturnDto() { Order = order };

			for (int i = 0; i < order.OrderItems.Count; i++)
			{
				var product = await _productService.ProductById(order.OrderItems[i].ProductId);
				if (product is null) { 
					orderToReturnDto.Messages.Add($"Product with Id : {order.OrderItems[i].ProductId} is not exist");
					continue;
				}
				if (product?.Stock < order.OrderItems[i].Quantity)
				{ 
					orderToReturnDto.Messages.Add($"Product : {product?.Name} is not sufficient for the requested quantity.");
					continue;
				}
				order.OrderItems[i].UnitPrice = product.Price;
				order.TotalAmount += (product.Price* order.OrderItems[i].Quantity);
			}
			if (orderToReturnDto.Messages.Count > 0)
				return orderToReturnDto;
			

			ApplyDiscount(order);
			order.OrderDate = DateTime.Now;
			var existedOrder = await _orderRepository.AddOrder(order);
			if (existedOrder == null)
			{
				orderToReturnDto.Messages.Add($"Couldn't create your order");
				return orderToReturnDto;
			}

			for (int i = 0; i < order.OrderItems.Count; i++)
			{
				var product = await _productService.ProductById(order.OrderItems[i].ProductId);
				product.Stock -= order.OrderItems[i].Quantity;
				await _productService.UpdateProduct(product);

				//order.OrderItems[i].OrderId = existedOrder.Id;
				//await _orderItemService.AddOrderItem(order.OrderItems[i]);
			}

			await GenerateInvoice(existedOrder);

			orderToReturnDto.Messages.Add("Your order is created successfully");
			orderToReturnDto.Order = existedOrder;
			return orderToReturnDto;
		}

		public async Task<IEnumerable<Order>> GetAllOrders()
		{
			return await _orderRepository.GetAllOrders();
		}

		public async Task<Order?> OrderById(int id)
		{
			return await _orderRepository.OrderById(id);
		}

		public async Task<Order?> UpdateOrder(int orderId, string status)
		{
			var order =  await _orderRepository.UpdateOrder(orderId, status);
			var customer =await _customerService.GetCustomerById(order.Customerid);
			await _emailService.SendEmailAsync(customer.Email, "Order status change", $"Your order status has been changed to: {status}");
			return order;
		}


		//helper methods
		public void ApplyDiscount(Order order)
		{
			if (order.TotalAmount > 100 && order.TotalAmount < 200)
				order.TotalAmount -= order.TotalAmount * 0.05m;

			if (order.TotalAmount > 200)
				order.TotalAmount -= order.TotalAmount * 0.1m;
		}

		public async Task GenerateInvoice(Order order)
		{
			var invoice = new Invoice
			{
				InvoiceDate = DateTime.Now,
				TotalAmount = order.TotalAmount,
				Name = order.Name,
				OrderId = order.Id
			};
			await _invoiceService.AddInvoice(invoice);
		}
	}
}
