using MedicalAppointmentSystem.Api.Dtos;
using MedicalAppointmentSystem.Api.Models.Entities;

namespace MedicalAppointmentSystem.Api.Services.Interfaces
{
    public interface IPrescriptionService
    {
        Task<Prescription> CreatePrescriptionAsync(CreatePrescriptionDto prescriptionDto);
        Task<bool> UpdatePrescriptionAsync(string id, CreatePrescriptionDto prescriptionDto);
    }
}
