using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
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

        public Station? Station { get; set; }
        public List<Package> AvailablePackages { get; set; } = new();

        public IActionResult OnGet(int id)
        {
            Station = _context.Stations
                .Include(s => s.StationPackages)
                .ThenInclude(sp => sp.Package)
                .FirstOrDefault(s => s.Id == id);

            if (Station == null)
            {
                return NotFound();
            }

            AvailablePackages = Station.StationPackages
                .Select(sp => sp.Package)
                .Distinct()
                .ToList();

            return Page();
        }
    }
}

