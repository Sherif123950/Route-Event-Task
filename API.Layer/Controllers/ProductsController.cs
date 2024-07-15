using AutoMapper;
using DataAccessLayer.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ServiceLayer.Dtos;
using ServiceLayer.Interfaces;

namespace API.Layer.Controllers
{
	[Route("api/products")]
	[ApiController]
	[Authorize]
	public class ProductsController : ControllerBase
	{
		private readonly IProductService _service;
		private readonly IMapper _mapper;

		public ProductsController(IProductService service, IMapper mapper)
		{
			this._service = service;
			this._mapper = mapper;
		}
		[HttpPost]
		[Authorize(Roles ="Admin")]
		public async Task<IActionResult> CreateProduct(ProductDto dto)
		{
			var product = _mapper.Map<ProductDto, Product>(dto);
			var result = await _service.AddProduct(product);
			if (result == null)
			{
				return BadRequest("Couldn't create Product");
			}
			return Ok(result);
		}

		[HttpGet("{ProductId}")]
		public async Task<IActionResult> GetProductById(int ProductId)
		{
			var Product = await _service.ProductById(ProductId);
			if (Product is null)
			{
				return NotFound("Couldn't find any Product with this id");
			}
			var ProductDto = _mapper.Map<Product, ProductDto>(Product);
			return Ok(ProductDto);
		}

		[HttpGet]
		public async Task<IActionResult> GetAllProduct()
		{
			var products = await _service.GetAllProducts();
			if (products is null)
			{
				return NotFound("Couldn't find any Products");
			}
			var productsDto = _mapper.Map<IEnumerable<Product>, IEnumerable<ProductDto>>(products);
			return Ok(productsDto);
		}

		[Authorize(Roles ="Admin")]
		[HttpPut("{productId}")]
		public async Task<IActionResult> UpdateProduct(int productId,[FromBody] ProductDto dto )
		{
            if (productId!=dto.Id)
            {
				return BadRequest("Ids didn't match between URL and body");
            }
            var product = _mapper.Map<ProductDto, Product>(dto);
			var result = await _service.UpdateProduct(product);
			if (result == null)
			{
				return NotFound($"There is no product with this Id : {productId}");
			}
			return Ok(result);
		}
	}
}
