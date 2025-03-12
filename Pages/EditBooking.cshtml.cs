using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using WashOverflowV2.Data;
using WashOverflowV2.Models;

namespace WashOverflowV2.Pages
{
    public class EditBookingModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public EditBookingModel(ApplicationDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        [BindProperty]
        public Booking Booking { get; set; }

        public List<Station> Stations { get; set; } = new List<Station>();
        public List<Package> Packages { get; set; } = new List<Package>();
        public List<Booking> Bookings { get; set; } = new List<Booking>();
      
        public async Task<IActionResult> OnPostAsync()
        {
            Stations = await _context.Stations.ToListAsync();
            Packages = await _context.Packages.ToListAsync();

            try
            {
                // Fetch the existing booking from the database
                var existingBooking = await _context.Bookings.FindAsync(Booking.Id);

                if (existingBooking == null)
                {
                    return NotFound();
                }

                // Ensure the current user owns the booking
                var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                if (existingBooking.UserId != userId && !User.IsInRole("Admin"))
                {
                    return Forbid();
                }

                // Update the booking properties
                existingBooking.StationId = Booking.StationId;
                existingBooking.PackageId = Booking.PackageId;
                existingBooking.Date = Booking.Date;
                existingBooking.RegistrationNumber = Booking.RegistrationNumber;

                // Mark the entity as modified and save changes
                _context.Attach(existingBooking).State = EntityState.Modified;
                await _context.SaveChangesAsync();

                TempData["SuccessMessage"] = "Booking updated successfully!";
                if (User.IsInRole("Admin"))
                {
                    return RedirectToPage("/AdminDashboard");
                }
                else
                {
                    return RedirectToPage("/MyBookingsPage");
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "An error occurred while updating the booking. Please try again.");
                Console.WriteLine($"Exception: {ex.Message}");
                Console.WriteLine($"Stack Trace: {ex.StackTrace}");
                return Page();
            }
        }
    }
}