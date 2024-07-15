using DataAccessLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Interfaces
{
	public interface IinvoiceService
	{
		public Task<int> AddInvoice(Invoice invoice);
		public Task<IEnumerable<Invoice>> GetAllInvoices();
		public Task<Invoice?> InvoiceById(int id);
	}
}
