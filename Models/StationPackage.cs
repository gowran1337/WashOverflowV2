namespace WashOverflowV2.Models
{
    public class StationPackage
    {
        public int StationId { get; set; }
        public Station Station { get; set; } = null!;
        public int PackageId { get; set; }
        public Package Package { get; set; } = null!;
    }
}
