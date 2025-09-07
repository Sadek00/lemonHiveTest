namespace MedicalAppointmentSystem.Api.Models.Entities
{
    public class Patient : BaseEntity
    {
        public required string PatientName { get; set; }
        public string? PatientEmail { get; set; }
        public string? PatientPhone { get; set; }
        public string? PatientAddress { get; set; }
        public virtual ICollection<Appointment> Appointments { get; set; } = [];
    }
}
