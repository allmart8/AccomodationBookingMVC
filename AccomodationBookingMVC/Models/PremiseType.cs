namespace AccomodationBookingMVC.Models
{
    public partial class PremiseType
    {
        public int PremiseTypeId { get; set; }
        public string TypeName { get; set; } = null!;
        public string? TypeDescription { get; set; }

        public virtual ICollection<Premise> Premises { get; set; } = new List<Premise>();
    }
}
