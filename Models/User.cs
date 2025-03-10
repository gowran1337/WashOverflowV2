using Microsoft.AspNetCore.Identity;

namespace WashOverflowV2.Models
{
    public class User : IdentityUser
    {
        public ICollection<Booking> Bookings { get; set; } = new List<Booking>();
    }
}
