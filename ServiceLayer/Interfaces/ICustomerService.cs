using DataAccessLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Interfaces
{
	public interface ICustomerService
	{
		public Task<Customer?> AddCustomer(Customer customer);
		public Task<IEnumerable<Order>> GetCustomerOrders(int customerId);
		public Task<Customer?> GetCustomerById(int id);
	}
}
