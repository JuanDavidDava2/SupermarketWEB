using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SupermarketWEB.Data;
using SupermarketWEB.Models;

namespace SupermarketWEB.Pages.Invoices
{
    public class EditModel : PageModel
    {
		private readonly SupermarketContext _context;

		public EditModel(SupermarketContext context)
		{
			_context = context;
		}

		[BindProperty]
		public Invoice Invoice { get; set; } = default!;

		public async Task<IActionResult> OnGetAsync(int? id)
		{
			if (id == null || _context.Invoices == null)
			{
				return NotFound();
			}

			var invoice = await _context.Invoices.FirstOrDefaultAsync(m => m.Id == id);
			if (invoice == null)
			{
				return NotFound();
			}
			Invoice = invoice;
			return Page();
		}

		public async Task<IActionResult> OnPostAsync()
		{
			if (!ModelState.IsValid)
			{
				return Page();
			}

			_context.Attach(Invoice).State = EntityState.Modified;

			try
			{
				await _context.SaveChangesAsync();
			}
			catch (DbUpdateConcurrencyException)
			{
				if (!InvoiceExists(Invoice.Id))
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

		private bool InvoiceExists(int id)
		{
			return (_context.Invoices?.Any(e => e.Id == id)).GetValueOrDefault();
		}
	}
}
