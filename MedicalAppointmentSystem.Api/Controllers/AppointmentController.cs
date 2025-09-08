using MedicalAppointmentSystem.Api.Dtos;
using MedicalAppointmentSystem.Api.Models.Entities;
using MedicalAppointmentSystem.Api.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace MedicalAppointmentSystem.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AppointmentController : ControllerBase
    {
        private readonly IAppointmentService _appointmentService;

        public AppointmentController(IAppointmentService appointmentService)
        {
            _appointmentService = appointmentService;
        }

        // GET: api/appointment
        [HttpGet]
        public async Task<ActionResult<PagedResult<AppointmentDto>>> GetAppointments([FromQuery] AppointmentQueryParameters queryParams)
        {
            try
            {
                if (queryParams.Page < 1) queryParams.Page = 1;
                if (queryParams.PageSize < 1 || queryParams.PageSize > 100) queryParams.PageSize = 10;

                var result = await _appointmentService.GetAppointmentsAsync(queryParams);
                return Ok(result);
            }
            catch (Exception ex)
            {
               return StatusCode(500, "An error occurred while retrieving appointments");
            }
        }

        // GET: api/appointment/5
        [HttpGet("{id}")]
        public async Task<ActionResult<AppointmentDto>> GetAppointment(string id)
        {
            try
            {
                var appointment = await _appointmentService.GetAppointmentByIdAsync(id);
                if (appointment == null)
                {
                    return NotFound();
                }
                return Ok(appointment);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while retrieving the appointment");
            }
        }

        // POST: api/appointment
        [HttpPost]
        public async Task<ActionResult<AppointmentDto>> CreateAppointment([FromBody] CreateAppointmentDto appointmentDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var appointment = await _appointmentService.CreateAppointmentAsync(appointmentDto);
                return Ok(appointment);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while creating the appointment");
            }
        }

        // PUT: api/appointment/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAppointment(string id, CreateAppointmentDto appointmentDto)
        {
            try
            {
                var updated = await _appointmentService.UpdateAppointmentAsync(id, appointmentDto);
                if (!updated)
                {
                    return NotFound();
                }
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while updating the appointment");
            }
        }

        // DELETE: api/appointment/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAppointment(string id)
        {
            try
            {
                var deleted = await _appointmentService.DeleteAppointmentAsync(id);
                if (!deleted)
                {
                    return NotFound();
                }
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while deleting the appointment");
            }
        }
    }
}
