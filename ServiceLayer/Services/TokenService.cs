using DataAccessLayer.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using ServiceLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Services
{
	public class TokenService : ITokenService
	{
		private readonly IConfiguration _configuration;

		public TokenService(IConfiguration configuration)
        {
			this._configuration = configuration;
		}
        public async Task<string> CreateToken(User user, UserManager<User> userManager)
		{
			var userClaims =await userManager.GetClaimsAsync(user);
			var authClaims = new List<Claim>()
			{
				new Claim(ClaimTypes.GivenName,user.UserName)
				//,
				//new Claim(ClaimTypes.Email,user.Email)
			};
			var userRoles = await userManager.GetRolesAsync(user);
            foreach (var role in userRoles)
            {
				authClaims.Add(new Claim(ClaimTypes.Role, role));
            }
			var allClaims = userClaims.Union(authClaims);
			var AuthKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:SecurityKey"]));
			var token = new JwtSecurityToken(
				issuer: _configuration["JWT:Issuer"],
				audience: _configuration["JWT:Audience"],
				expires:DateTime.Now.AddDays(double.Parse(_configuration["JWT:DurationInDays"])),
				claims: authClaims,
				signingCredentials:new SigningCredentials(AuthKey,SecurityAlgorithms.HmacSha256Signature)
				);
			return new JwtSecurityTokenHandler().WriteToken(token);
        }
	}
}
