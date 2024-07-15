using DataAccessLayer.Data.Contexts;
using DataAccessLayer.Entities;
using Microsoft.EntityFrameworkCore;
using RepositoryLayer.Interfaces;
using ServiceLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Services
{
	public class CustomerService : ICustomerService
	{
		private readonly ICustomerRepository _customerRepository;

		public CustomerService(ICustomerRepository customerRepository)
        {
			this._customerRepository = customerRepository;
		}
        public async Task<Customer?> AddCustomer(Customer customer)
		{
			return await _customerRepository.AddCustomer(customer);
		}

		public async Task<IEnumerable<Order>> GetCustomerOrders(int customerId)
		{
			return await _customerRepository.GetCustomerOrders(customerId);
		}
		public async Task<Customer?> GetCustomerById(int id)
		{
			return await _customerRepository.GetCustomerById(id);
		}
	}
}
