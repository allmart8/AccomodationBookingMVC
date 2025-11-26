namespace AccomodationBookingMVC.Models
{
    public partial class Premise
    {
        public int PremiseId { get; set; }
        public int OwnerId { get; set; }
        public int PremiseTypeId { get; set; }

        public string PremiseName { get; set; } = null!;
        public string Address { get; set; } = null!;
        public int RoomCount { get; set; }
        public float Area { get; set; }
        public bool HasRestroom { get; set; }

        public virtual Owner Owner { get; set; } = null!;
        public virtual PremiseType PremiseType { get; set; } = null!;

        public virtual ICollection<RentalAgreement> RentalAgreements { get; set; } = new List<RentalAgreement>();
        public virtual ICollection<Service> Services { get; set; } = new List<Service>();
    }
}
