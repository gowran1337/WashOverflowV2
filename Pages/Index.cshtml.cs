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
        public int? SelectedPackageId { get; set; } // Paket som anv�ndaren valt

        public void OnGet()
        {
            // H�mta alla paket f�r dropdown-listan
            Packages = _context.Packages.ToList();

            // Om anv�ndaren har valt ett paket, filtrera stationerna
            if (SelectedPackageId.HasValue && SelectedPackageId > 0)
            {
                Stations = _context.Stations
                    .Where(s => s.StationPackages.Any(sp => sp.PackageId == SelectedPackageId))
                    .ToList();
            }
            else
            {
                Stations = _context.Stations.ToList(); // Visa alla stationer om inget paket �r valt
            }
        }

        public IActionResult OnPostBookPage()
        {
            return RedirectToPage("BookPage"); // Redirects to BookPage.cshtml
        }
    }

}

