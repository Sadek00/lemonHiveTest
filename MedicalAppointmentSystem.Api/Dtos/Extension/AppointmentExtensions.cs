using MedicalAppointmentSystem.Api.Models.Entities;

namespace MedicalAppointmentSystem.Api.Dtos.Extension
{
    public static class AppointmentExtensions
    {
        public static AppointmentDto ToDto(this Appointment appointment)
        {
            if (appointment == null) return new AppointmentDto();

            return new AppointmentDto
            {
                PatientId = appointment.PatientId,
                DoctorId = appointment.DoctorId,
                AppointmentDate = appointment.AppointmentDate,
                VisitType = appointment.VisitType,
                Notes = appointment.Notes,
                Diagnosis = appointment.Diagnosis,
                CreatedDate = appointment.CreatedDate,
                Prescriptions = appointment.Prescriptions?.Select(p => new PrescriptionDto
                {
                    MedicineId = p.MedicineId,
                    Dosage = p.Dosage,
                    StartDate = p.StartDate,
                    EndDate = p.EndDate,
                    Notes = p.Notes,
                    CreatedDate = DateTime.Now
                }).ToList() ?? []
            };
        }
    }
}
