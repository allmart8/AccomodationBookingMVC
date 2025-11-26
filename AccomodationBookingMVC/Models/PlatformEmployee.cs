using System.ComponentModel.DataAnnotations;

namespace AccomodationBookingMVC.Models
{
    public partial class PlatformEmployee
    {
        [Key]
        public int EmployeeId { get; set; }
        public string FullName { get; set; } = null!;
        public string? Position { get; set; }
        public string Email { get; set; } = null!;
        public DateOnly HireDate { get; set; }

        public virtual ICollection<RentalAgreement> RentalAgreements { get; set; } = new List<RentalAgreement>();
    }
}
