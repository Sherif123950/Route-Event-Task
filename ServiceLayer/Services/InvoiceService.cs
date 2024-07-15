using DataAccessLayer.Data.Contexts;
using DataAccessLayer.Entities;
using Microsoft.EntityFrameworkCore;
using RepositoryLayer.Interfaces;
using ServiceLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Services
{
	
	public class InvoiceService : IinvoiceService
	{
		private readonly IinvoiceRepository _repository;
		//private readonly IOrderService _orderService;

		public InvoiceService(IinvoiceRepository repository/*,IOrderService orderService*/)
		{
			this._repository = repository;
			//this._orderService = orderService;
		}
		public async Task<int> AddInvoice(Invoice invoice)
		{
			return await _repository.AddInvoice(invoice);
		}

		public async Task<IEnumerable<Invoice>> GetAllInvoices()
		{
			var invoices = await _repository.GetAllInvoices();
            
            foreach (var item in invoices)
            {
				//var order =await _orderService.OrderById(item.OrderId);
				//item.Order = order;
            }
            return invoices;
		}

		public async Task<Invoice?> InvoiceById(int id)
		{
			var invoice = await _repository.InvoiceById(id);
			//var order = await _orderService.OrderById(invoice.OrderId);
			//invoice.Order = order;
			return invoice;
		}
	}
}
