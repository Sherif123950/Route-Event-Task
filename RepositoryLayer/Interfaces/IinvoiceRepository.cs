using DataAccessLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.Interfaces
{
	public interface IinvoiceRepository
	{
		public Task<int> AddInvoice(Invoice invoice);
		public Task<IEnumerable<Invoice>> GetAllInvoices();
		public Task<Invoice?> InvoiceById(int id);
	}
}
