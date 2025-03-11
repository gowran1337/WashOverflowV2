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

        public IReadOnlyDictionary<string, string> FeatureImages { get; } = new Dictionary<string, string>
        {
            { "Utv�ndig tv�tt", "exterior-wash.png" },
            { "Inv�ndig reng�ring", "interior-cleaning.png" },
            { "Vaxning", "waxing.png" },
            { "D�ckglans", "tire-shine.png" },
            { "F�nsterputs", "window-cleaning.png" },
            { "Motortv�tt", "engine-wash.png" },
            { "Interi�r desinficering", "interior-disinfection.png" },
            { "Luktsanering", "odor-removal.png" },
            { "Keramisk bel�ggning", "ceramic-coating.png" }
        };

        public Station? Station { get; set; }
        public List<Package> AvailablePackages { get; set; } = new();

        public IActionResult OnGet(int id)
        {
            Station = _context.Stations
                .Include(s => s.StationPackages)
                .ThenInclude(sp => sp.Package)
                .ThenInclude(p => p.PackageFeatures) // Inkludera kopplingen till features
                .ThenInclude(pf => pf.Feature) // Inkludera sj�lva feature-data
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

