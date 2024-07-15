using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Dtos
{
	public class RegisterDto
	{
		public string Role { get; set; }
		public string UserName { get; set; }
		public string Password { get; set; }
	}
}
