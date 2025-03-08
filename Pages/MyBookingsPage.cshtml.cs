using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using WashOverflowV2.Data;
using WashOverflowV2.Models;

namespace WashOverflowV2.Pages
{
    public class MyBookingsPageModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public MyBookingsPageModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public List<Booking> Bookings { get; set; }

        public async Task OnGetAsync()
        {
            Bookings = await _context.Bookings
                .Include(b => b.Station)
                .Include(b => b.Package)
                .ToListAsync();
        }
    }
}

