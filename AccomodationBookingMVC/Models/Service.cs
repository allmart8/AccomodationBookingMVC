namespace AccomodationBookingMVC.Models
{
    public partial class Service
    {
        public int ServiceId { get; set; }
        public string ServiceName { get; set; } = null!;
        public string? ServiceDescription { get; set; }

        public virtual ICollection<Premise> Premises { get; set; } = new List<Premise>();
    }
}
