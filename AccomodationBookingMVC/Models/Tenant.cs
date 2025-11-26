namespace AccomodationBookingMVC.Models
{
    public partial class Tenant
    {
        public int TenantId { get; set; }
        public string FullName { get; set; } = null!;
        public string? Gender { get; set; }
        public DateOnly BirthDate { get; set; }

        public string PhoneNumber { get; set; } = null!;
        public string PassportData { get; set; } = null!;
        public decimal? MaxPrice { get; set; }
        public string? AdditionalWishes { get; set; }

        public virtual ICollection<RentalAgreement> RentalAgreements { get; set; } = new List<RentalAgreement>();
    }
}
