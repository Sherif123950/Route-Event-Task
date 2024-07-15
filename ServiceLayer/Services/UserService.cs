using DataAccessLayer.Data.Contexts;
using DataAccessLayer.Entities;
using Microsoft.AspNetCore.Identity;
using RepositoryLayer.Interfaces;
using ServiceLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Repositpries
{
	internal class UserService : IUserService
	{
		private readonly OrderManagementDbContext _dbContext;
		private readonly UserManager<User> _userManager;
		private readonly RoleManager<IdentityRole> _roleManager;

		public UserService(OrderManagementDbContext dbContext, UserManager<User> userManager, RoleManager<IdentityRole> roleManager)
		{
			this._dbContext = dbContext;
			this._userManager = userManager;
			this._roleManager = roleManager;
		}
		public Task<Order> Login(User user)
		{
			throw new NotImplementedException();
		}

		public async Task<int> Register(User user, string password)
		{
			var result = await _userManager.CreateAsync(user, password);
			if (result.Succeeded)
			{
				return await _dbContext.SaveChangesAsync();
			}
			throw new ApplicationException("Error creating user.");
		}
	}
}
