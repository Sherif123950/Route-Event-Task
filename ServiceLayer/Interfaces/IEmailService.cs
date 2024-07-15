﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Interfaces
{
	public interface IEmailService
	{
		Task SendEmailAsync(string emailTo, string subject, string body);	
	}
}
