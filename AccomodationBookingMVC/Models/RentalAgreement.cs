using System.ComponentModel.DataAnnotations;

namespace AccomodationBookingMVC.Models
{
    public partial class RentalAgreement
    {
        [Key]
        public long AgreementId { get; set; }

        public int PremiseId { get; set; }
        public int TenantId { get; set; }

        public DateOnly ConclusionDate { get; set; }
        public DateOnly RentalStartDate { get; set; }
        public DateOnly RentalEndDate { get; set; }

        public decimal TotalSum { get; set; }

        public int? EmployeeId { get; set; }

        public virtual PlatformEmployee? Employee { get; set; }
        public virtual Premise Premise { get; set; } = null!;
        public virtual Tenant Tenant { get; set; } = null!;
    }
}
