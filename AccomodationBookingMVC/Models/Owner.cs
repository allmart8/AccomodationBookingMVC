namespace AccomodationBookingMVC.Models
{
    public partial class Owner
    {
        public int OwnerId { get; set; }
        public string FullName { get; set; } = null!;
        public string? Gender { get; set; }
        public DateOnly BirthDate { get; set; }
        public string PhoneNumber { get; set; } = null!;
        public string PassportData { get; set; } = null!;

        public virtual ICollection<Premise> Premises { get; set; } = new List<Premise>();
    }
}
