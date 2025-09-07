using MedicalAppointmentSystem.Api.Models.Enums;

namespace MedicalAppointmentSystem.Api.Dtos
{
    public class AppointmentDto
    {
        public string PatientId { get; set; } = null!;
        public string DoctorId { get; set; } = null!;
        public DateTime AppointmentDate { get; set; }
        public VisitTypeEnum VisitType { get; set; }
        public string? Notes { get; set; }
        public string? Diagnosis { get; set; }
        public DateTime? CreatedDate { get; set; }
        public List<PrescriptionDto> Prescriptions { get; set; } = new List<PrescriptionDto>();
    }
}
