using MedicalAppointmentSystem.Api.Models.Enums;

namespace MedicalAppointmentSystem.Api.Models.Entities
{
    public class Appointment : BaseEntity
    {
        public required string PatientId { get; set; }
        public required string DoctorId { get; set; }
        public DateTime AppointmentDate { get; set; }
        public VisitTypeEnum VisitType { get; set; }
        public string? Notes { get; set; }
        public string? Diagnosis { get; set; }

        // Navigation properties
        public virtual Patient Patient { get; set; } = null!;
        public virtual Doctor Doctor { get; set; } = null!;

        public virtual ICollection<Prescription> Prescriptions { get; set; } = [];
    }
}
