using AutoMapper;
using DataAccessLayer.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ServiceLayer.Dtos;
using ServiceLayer.Interfaces;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace API.Layer.Controllers
{
	[Route("api/orders")]
	[ApiController]
	[Authorize]
	public class OrderController : ControllerBase
	{
		private readonly IOrderService _service;
		private readonly IMapper _mapper;

		public OrderController(IOrderService service,IMapper mapper)
        {
			this._service = service;
			this._mapper = mapper;
		}
        //		POST /api/orders - Create a new order
        [HttpPost]
		public async Task<IActionResult> CreateOrder(OrderDto dto)
		{
			var order = _mapper.Map<OrderDto, Order>(dto);
			var result =await  _service.AddOrder(order); //I handelled all  bussiness cases in order service
			if (result.Messages.Contains("Couldn't create your order")) 
            {
				return BadRequest(result);
            }
            return Ok(result);
        }

		//o GET /api/orders/{orderId} - Get details of a specific order
		[HttpGet("{orderId}")]
		public async Task<IActionResult> GetOrderById(int orderId)
		{
			var order  = await _service.OrderById(orderId);
            if (order is null)
            {
				return NotFound($"Couldn't find any order with this id : {orderId}");
            }
            var orderDto = _mapper.Map<Order, OrderDto>(order);
			return Ok(orderDto);
		}

		//o GET /api/orders - Get all orders(admin only)
		[HttpGet]
		[Authorize(Roles ="Admin")]
		public async Task<IActionResult> GetAllOrder()
		{
			var orders = await _service.GetAllOrders();
			if (orders is null)
			{
				return NotFound("Couldn't find any orders");
			}
			var ordersDto = _mapper.Map<IEnumerable<Order>,IEnumerable<OrderDto>>(orders);
			return Ok(ordersDto);
		}

		//o PUT /api/orders/{orderId}/ status - Update order status(admin only)
		[HttpPut("{orderId}/{status}")]
		[Authorize(Roles = "Admin")]
		public async Task<IActionResult> UpdateStatus(int orderId, string status)
		{
			var result = await _service.UpdateOrder( orderId,  status);
            if (result==null)
            {
				return NotFound($"There is no Order with this id {orderId}");
            }
			return Ok(result);
        }

	}
}
