using AutoMapper;
using DataAccessLayer.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ServiceLayer.Dtos;
using ServiceLayer.Interfaces;

namespace API.Layer.Controllers
{
	[Route("api/customers")]
	[ApiController]
	[Authorize]
	public class CustomerController : ControllerBase
	{
		private readonly IMapper _mapper;
		private readonly ICustomerService _service;

		public CustomerController(IMapper mapper,ICustomerService service)
        {
			this._mapper = mapper;
			this._service = service;
		}
        //POST /api/customers - Create a new customer
        [HttpPost]
		public async Task<IActionResult> CreateCustomer(CustomerDto dto)
		{
			var customer = _mapper.Map<CustomerDto,Customer>(dto);
			var result =await  _service.AddCustomer(customer);
			if (result == null)
				return BadRequest("Couldn't Create Customer");
            return Ok(result);
		}

		//o GET /api/customers/{customerId}/orders - Get all orders for a customer
		[HttpGet("{customerId}/orders")]
		public async Task<IActionResult> GetCustomerOrders(int customerId)
		{
			var orders=await _service.GetCustomerOrders(customerId);
			if (orders == null)
				return NotFound("There is no Orders Found for this customer");
            var ordersDto = _mapper.Map<IEnumerable<Order>, IEnumerable<OrderDto>>(orders);
			return Ok(ordersDto);
		}
		
	}
}
