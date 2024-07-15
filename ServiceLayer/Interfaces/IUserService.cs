using DataAccessLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Interfaces
{
	public interface IUserService
	{
		public Task<int> Register(User user, string password);
		public Task<Order> Login(User user);
		
	}
}
