using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WashOverflowV2.Data;
using WashOverflowV2.Models;

namespace WashOverflowV2.Pages
{
    public class MyBookingsModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public MyBookingsModel(ApplicationDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
            UserBookings = new List<Booking>();
        }

        public List<Booking> UserBookings { get; set; } //creates a local list of database items

        public async Task<IActionResult> OnGetAsync()
        {
            var user = await _userManager.GetUserAsync(User) as WashOverflowV2.Models.User;
            if (user == null)
            {
                return RedirectToPage("/Login");
            }

            UserBookings = await _context.Bookings
                .Where(b => b.User.Id == user.Id)
                .Include(b => b.Station)
                .Include(b => b.Package)
                .ToListAsync();

            return Page();
        }
        public async Task<IActionResult> OnPostDeleteAsync(int bookingId)
        {
            var booking = await _context.Bookings.FindAsync(bookingId);

            if (booking == null)
            {              
                return RedirectToPage();
            }

            _context.Bookings.Remove(booking);
            await _context.SaveChangesAsync();

            TempData["SuccessMessage"] = "Booking successfully deleted!";
            return RedirectToPage();
        }
    }
}
