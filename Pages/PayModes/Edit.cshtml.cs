using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SupermarketWEB.Data;
using SupermarketWEB.Models;

namespace SupermarketWEB.Pages.PayModes
{
	public class EditModel : PageModel
	{
		private readonly SupermarketContext _context;

		public EditModel(SupermarketContext context)
		{
			_context = context;
		}

		[BindProperty]
		public PayMode PayMode { get; set; } = default!;

		public async Task<IActionResult> OnGetAsync(int? id)
		{
			if (id == null)
			{
				return NotFound();
			}

			var paymode = await _context.PayModes.FirstOrDefaultAsync(m => m.Id == id);

			if (paymode == null)
			{
				return NotFound();
			}

			PayMode = paymode;
			return Page();
		}

		public async Task<IActionResult> OnPostAsync()
		{
			if (!ModelState.IsValid)
			{
				return Page();
			}

			// Verifica si el ID de la factura asignado a PayMode es válido
			var invoiceExists = await _context.Invoices.AnyAsync(i => i.Id == PayMode.InvoiceId);

			if (!invoiceExists)
			{
				ModelState.AddModelError(string.Empty, "La factura especificada no existe.");
				return Page();
			}

			_context.Attach(PayMode).State = EntityState.Modified;

			try
			{
				await _context.SaveChangesAsync();
			}
			catch (DbUpdateConcurrencyException)
			{
				if (!PayModeExists(PayMode.Id))
				{
					return NotFound();
				}
				else
				{
					throw;
				}
			}

			return RedirectToPage("./Index");
		}

		private bool PayModeExists(int id)
		{
			return _context.PayModes.Any(e => e.Id == id);
		}

	}
}
