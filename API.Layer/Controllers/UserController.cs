using DataAccessLayer.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Win32;
using ServiceLayer.Dtos;
using ServiceLayer.Interfaces;

namespace API.Layer.Controllers
{
	[Route("api/users")]
	[ApiController]
	public class UserController : ControllerBase
	{
		private readonly ITokenService _tokenService;
		private readonly UserManager<User> _userManager;
		private readonly SignInManager<User> _signInManager;

		public UserController(ITokenService tokenService, UserManager<User> userManager, SignInManager<User> signInManager)
		{
			this._tokenService = tokenService;
			this._userManager = userManager;
			this._signInManager = signInManager;
		}
		//o POST /api/users/register - Register a new user
		[HttpPost("register")]
		public async Task<IActionResult> Register(RegisterDto dto)
		{
			var userExisted = await _userManager.FindByNameAsync(dto.UserName);
			if (userExisted is not null)
			{
				return BadRequest("This username is used");
			}
			var user = new User();
			user.UserName=dto.UserName;


			var result = await _userManager.CreateAsync(user, dto.Password);
            if (!result.Succeeded)
            {
				return BadRequest("couldn't Create User");
            }
			var createdUser = await _userManager.FindByNameAsync(dto.UserName);
			await _userManager.AddToRoleAsync(createdUser, dto.Role);

			var userRoles =await _userManager.GetRolesAsync(createdUser);
			var userDto = new UserDto()
			{
				Roles = userRoles,
				Username = dto.UserName,
				Token =await _tokenService.CreateToken(createdUser, _userManager)
			};
			return Ok(userDto);
        }

		//o POST /api/users/login - Authenticate a user and return a JWT token
		[HttpPost("login")]
		public async Task<IActionResult> Login(LoginDto dto)
		{
			var userExist =await _userManager.FindByNameAsync(dto.UserName);
            if (userExist is null || !await _userManager.CheckPasswordAsync(userExist,dto.Password))
            {
				return BadRequest("Email or password is not correct");
            }
			var result = await _signInManager.CheckPasswordSignInAsync(userExist, dto.Password, false);
            if (!result.Succeeded)
            {
				return Unauthorized();
            }
			return Ok(new UserDto() { Username=dto.UserName,Token= await _tokenService.CreateToken(userExist, _userManager) , Roles=(await _userManager.GetRolesAsync(userExist)).ToList()});
        }
	}
}
