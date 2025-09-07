using MedicalAppointmentSystem.Api.Dtos;
using MedicalAppointmentSystem.Api.Models.Entities;

namespace MedicalAppointmentSystem.Api.Services.Interfaces
{
    public interface IAppointmentService
    {
        Task<IEnumerable<Appointment>> GetAllAppointmentsAsync();
        Task<Appointment?> GetAppointmentByIdAsync(string id);
        Task<AppointmentDto> CreateAppointmentAsync(CreateAppointmentDto appointmentDto);
        Task<bool> UpdateAppointmentAsync(string id, CreateAppointmentDto appointmentDto);
        Task<bool> DeleteAppointmentAsync(string id);
        Task<IEnumerable<Prescription>> GetAppointmentPrescriptionsAsync(string appointmentId);
        Task<bool> AppointmentExistsAsync(string id);
    }
}
