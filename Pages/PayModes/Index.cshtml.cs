using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SupermarketWEB.Data;
using SupermarketWEB.Models;

namespace SupermarketWEB.Pages.Paymodes
{
    public class IndexModel : PageModel
    {

        private readonly SupermarketContext _context;
        public IndexModel(SupermarketContext context)
        {
            _context = context;
        }
        public IList<PayMode> PayModes { get; set; }
        public async Task OnGetAsync()
        {
            PayModes = await _context.Pay_modes.ToListAsync();
        }


    }
}
