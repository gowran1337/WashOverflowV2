using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using WashOverflowV2.Data;
using WashOverflowV2.Models;

namespace WashOverflowV2.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;

        public IndexModel(
            ApplicationDbContext context,
            UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager)
        {
            _context = context;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public IList<Station> Stations { get; set; }
        public IList<Package> Packages { get; set; } = new List<Package>();

        [BindProperty(SupportsGet = true)]
        public int? SelectedPackageId { get; set; } // Paket som användaren valt


        public async Task<IActionResult> OnGetAsync()
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
            // Redirect admin users to the admin dashboard
            if (_signInManager.IsSignedIn(User))
            {
                var user = await _userManager.GetUserAsync(User);
                if (user != null && await _userManager.IsInRoleAsync(user, "Admin"))
                {
                    return RedirectToPage("/AdminDashboard");
                }
            }

            return Page();
        }

        public IActionResult OnPostBookPage()
        {
            return RedirectToPage("BookPage"); // Redirects to BookPage.cshtml
        }
    }
}
