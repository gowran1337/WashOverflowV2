using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using WashOverflowV2.Data;
using WashOverflowV2.Models;

namespace WashOverflowV2.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly ApplicationDbContext _context;

        public IndexModel(ILogger<IndexModel> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IList<Station> Stations { get; set; }
        public IList<Package> Packages { get; set; } = new List<Package>();

        [BindProperty(SupportsGet = true)]
        public int? SelectedPackageId { get; set; } // Paket som användaren valt

        public void OnGet()
        {
            // Hämta alla paket för dropdown-listan
            Packages = _context.Packages.ToList();

            // Om användaren har valt ett paket, filtrera stationerna
            if (SelectedPackageId.HasValue && SelectedPackageId > 0)
            {
                Stations = _context.Stations
                    .Where(s => s.StationPackages.Any(sp => sp.PackageId == SelectedPackageId))
                    .ToList();
            }
            else
            {
                Stations = _context.Stations.ToList(); // Visa alla stationer om inget paket är valt
            }
        }

        public IActionResult OnPostBookPage()
        {
            return RedirectToPage("BookPage"); // Redirects to BookPage.cshtml
        }
    }

}

