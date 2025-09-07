namespace MedicalAppointmentSystem.Api.Models.Entities
{
    public class Doctor: BaseEntity
    {
        public required string DoctorName { get; set; }
        public string? DoctorEmail { get; set; }
        public string? DoctorPhone { get; set; }
        public string? Specialization { get; set; }
        public virtual ICollection<Appointment> Appointments { get; set; } = [];
    }
}
