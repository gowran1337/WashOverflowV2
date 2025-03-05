using static System.Collections.Specialized.BitVector32;

namespace WashOverflowV2.Models
{
    public class Booking
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public int PackageId { get; set; }
        public int StationId { get; set; }
        public DateTime Date { get; set; }

        public User User { get; set; } = null!;
        public Package Package { get; set; } = null!;
        public Station Station { get; set; } = null!;
    }
}
