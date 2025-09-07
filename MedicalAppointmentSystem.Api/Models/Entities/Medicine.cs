namespace MedicalAppointmentSystem.Api.Models.Entities
{
    public class Medicine : BaseEntity
    {
        public required string MedicineName { get; set; }
        public string? Description { get; set; }

        public virtual ICollection<Prescription> Prescriptions { get; set; } = new List<Prescription>();
    }
}
