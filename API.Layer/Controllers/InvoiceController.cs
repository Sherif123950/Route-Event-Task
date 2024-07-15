using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ServiceLayer.Interfaces;

namespace API.Layer.Controllers
{
	[Route("api/invoices")]
	[ApiController]
	[Authorize(Roles = "Admin")]
	public class InvoiceController : ControllerBase
	{
		private readonly IMapper _mapper;
		private readonly IinvoiceService _service;

		public InvoiceController(IMapper mapper, IinvoiceService service)
		{
			this._mapper = mapper;
			this._service = service;
		}
		//o GET /api/invoices/{invoiceId} - Get details of a specific invoice(admin only)
		[HttpGet("{invoiceId}")]
		public async Task<IActionResult> GetInvoiceDetail(int invoiceId)
		{
			var invoice =await _service.InvoiceById(invoiceId);
            if (invoice is null)
            {
				return NotFound($"There is no invoice with this Id : {invoiceId}");
            }
			return Ok(invoice);
        }
		//o GET /api/invoices - Get all invoices(admin only)
		[HttpGet]
		public async Task<IActionResult> GetAllInvoice()
		{
			var invoices =await _service.GetAllInvoices();
			if (invoices is null)
			{
				return NotFound("there are no invoices");
			}
			return Ok(invoices);
		}
	}
}
