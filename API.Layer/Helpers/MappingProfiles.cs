using AutoMapper;
using DataAccessLayer.Entities;
using ServiceLayer.Dtos;

namespace API.Layer.Helpers
{
	public class MappingProfiles:Profile
	{
		public MappingProfiles()
		{
			CreateMap<Customer, CustomerDto>().ReverseMap();
			CreateMap<Product, ProductDto>().ReverseMap();
			CreateMap<Order, OrderDto>().ReverseMap();
			CreateMap<OrderItem, OrderItemDto>().ReverseMap();
		}
	}
}
