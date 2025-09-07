using MedicalAppointmentSystem.Api.Data.Context;
using MedicalAppointmentSystem.Api.Dtos;
using MedicalAppointmentSystem.Api.Models.Entities;
using MedicalAppointmentSystem.Api.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace MedicalAppointmentSystem.Api.Services.Implementation
{
    public class PrescriptionService : IPrescriptionService
    {
        private readonly AppDbContext _context;
        private readonly ILogger<PrescriptionService> _logger;

        public PrescriptionService(AppDbContext context, ILogger<PrescriptionService> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<Prescription> CreatePrescriptionAsync(CreatePrescriptionDto prescriptionDto)
        {
            try
            {
                // Validate appointment exists
                var appointmentExists = await _context.Appointments.AnyAsync(a => a.Id == prescriptionDto.AppointmentId);
                if (!appointmentExists)
                {
                    throw new ArgumentException("Invalid AppointmentId", nameof(prescriptionDto.AppointmentId));
                }

                // Validate medicine exists
                var medicineExists = await _context.Medicines.AnyAsync(m => m.Id == prescriptionDto.MedicineId);
                if (!medicineExists)
                {
                    throw new ArgumentException("Invalid MedicineId", nameof(prescriptionDto.MedicineId));
                }

                var prescription = new Prescription
                {
                    AppointmentId = prescriptionDto.AppointmentId,
                    MedicineId = prescriptionDto.MedicineId,
                    Dosage = prescriptionDto.Dosage,
                    StartDate = prescriptionDto.StartDate,
                    EndDate = prescriptionDto.EndDate,
                    Notes = prescriptionDto.Notes
                };

                _context.Prescriptions.Add(prescription);
                await _context.SaveChangesAsync();

                _logger.LogInformation("Created new prescription with ID {PrescriptionId}", prescription.Id);
                return prescription;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating prescription");
                throw;
            }
        }

        public async Task<bool> UpdatePrescriptionAsync(string id, CreatePrescriptionDto prescriptionDto)
        {
            try
            {
                var prescription = await _context.Prescriptions.FindAsync(id);
                if (prescription == null)
                {
                    return false;
                }

                prescription.Dosage = prescriptionDto.Dosage;
                prescription.StartDate = prescriptionDto.StartDate;
                prescription.EndDate = prescriptionDto.EndDate;
                prescription.Notes = prescriptionDto.Notes;

                await _context.SaveChangesAsync();
                _logger.LogInformation("Updated prescription with ID {PrescriptionId}", id);
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating prescription with ID {PrescriptionId}", id);
                throw;
            }
        }
    }

}
