using DataAccessLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.Interfaces
{
	public interface IUserRepository
	{
		public Task<int> Register(User user, string password);
		public Task<Order> Login(User user);
		
	}
}
