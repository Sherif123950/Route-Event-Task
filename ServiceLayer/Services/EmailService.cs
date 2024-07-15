using DataAccessLayer.Entities;
using Microsoft.Extensions.Options;
using ServiceLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Services
{
	public class EmailService : IEmailService
	{
		private readonly EmailSettings _emailSettings;

		public EmailService(IOptions<EmailSettings> options)
		{
			this._emailSettings = options.Value;
		}
		public async Task SendEmailAsync(string emailTo, string subject, string body)
		{
			var smtpClient = new SmtpClient(_emailSettings.SmtpServer, _emailSettings.SmtpPort)
			{

				Credentials = new NetworkCredential(_emailSettings.SmtpUser, _emailSettings.SmtpPass),
				EnableSsl = true
			};
			var mailMessage = new MailMessage()
			{
				From = new MailAddress(_emailSettings.SenderEmail, _emailSettings.SenderName),
				Subject = subject,
				IsBodyHtml = true,
				Body = body
			};
			mailMessage.To.Add(new MailAddress(emailTo));
			await smtpClient.SendMailAsync(mailMessage);
		}
	}
}
