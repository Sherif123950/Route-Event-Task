using DataAccessLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Dtos
{
	public class OrderToReturnDto
	{
		public List<string> Messages { get; set; } = new List<string>();
		public Order Order { get; set; }
	}
}
