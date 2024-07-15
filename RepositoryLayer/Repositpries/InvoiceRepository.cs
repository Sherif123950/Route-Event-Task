using DataAccessLayer.Data.Contexts;
using DataAccessLayer.Entities;
using Microsoft.EntityFrameworkCore;
using RepositoryLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.Repositpries
{
	
	public class InvoiceRepository : IinvoiceRepository
	{
		private readonly OrderManagementDbContext _dbContext;

		public InvoiceRepository(OrderManagementDbContext dbContext)
		{
			this._dbContext = dbContext;
		}
		public async Task<int> AddInvoice(Invoice invoice)
		{
			await _dbContext.Invoices.AddAsync(invoice);
			return await _dbContext.SaveChangesAsync();
		}

		public async Task<IEnumerable<Invoice>> GetAllInvoices()
		{
			return await _dbContext.Invoices.ToListAsync();
		}

		public async Task<Invoice?> InvoiceById(int id)
		{
			return await _dbContext.Invoices.FirstOrDefaultAsync(inv => inv.Id == id);
		}
	}
}
