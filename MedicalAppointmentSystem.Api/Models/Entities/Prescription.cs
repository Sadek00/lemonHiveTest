namespace MedicalAppointmentSystem.Api.Models.Entities
{
    public class Prescription : BaseEntity
    {
        public required string AppointmentId { get; set; }
        public required string MedicineId { get; set; }
        public string? Dosage { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string? Notes { get; set; }

        // Navigation properties
        public virtual Appointment Appointment { get; set; } = null!;
        public virtual Medicine Medicine { get; set; } = null!;
    }
}
