using MedicalAppointmentSystem.Api.Models.Enums;
using System.ComponentModel.DataAnnotations;

namespace MedicalAppointmentSystem.Api.Dtos
{
    public class CreateAppointmentDto
    {
        [Required]
        public required string PatientId { get; set; }

        [Required]
        public required string DoctorId { get; set; }

        [Required]
        public DateTime AppointmentDate { get; set; }

        [Required]
        public VisitTypeEnum VisitType { get; set; }

        public string? Notes { get; set; }
        public string? Diagnosis { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;

        public List<CreatePrescriptionForAppointmentDto>? Prescriptions { get; set; }
    }
}
