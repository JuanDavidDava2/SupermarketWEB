using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SupermarketWEB.Data;
using SupermarketWEB.Models;

namespace SupermarketWEB.Pages.Invoices
{
    public class CreateModel : PageModel
    {
		private readonly SupermarketContext _context;
		public CreateModel(SupermarketContext context)
		{
			_context = context;
		}

		public IActionResult OnGet()
		{
			return Page();
		}

		[BindProperty]
		public Invoice Invoice { get; set; } = default!;

		public async Task<IActionResult> OnPostAsync()
		{
			if (!ModelState.IsValid || _context.Invoices == null || Invoice == null)
			{
				return Page();
			}

			_context.Invoices.Add(Invoice);

			await _context.SaveChangesAsync();

			return RedirectToPage("./Index");
		}
	}
}
