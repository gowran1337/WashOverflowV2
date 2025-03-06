using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using WashOverflowV2.Data;
using WashOverflowV2.Models;

namespace WashOverflowV2.Pages
{
    public class DetailsCarwashModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public DetailsCarwashModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public Station Station { get; set; }

        public IActionResult OnGet(int id)
        {
            Station = _context.Stations
                .Include(s => s.Packages)
                .ThenInclude(p => p.PackageFeatures)
                .ThenInclude(pf => pf.Feature)
                .FirstOrDefault(s => s.Id == id);

            if (Station == null)
            {
                return NotFound();
            }

            return Page();
        }
    }
}
