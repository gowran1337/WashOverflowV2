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

        public void OnGet()
        {
            Stations = _context.Stations.ToList();
        }

        public IActionResult OnPostBookPage()
        {
            return RedirectToPage("BookPage"); // Redirects to BookPage.cshtml
        }
    }

}

