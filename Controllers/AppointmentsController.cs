using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AutoService.Data;
using AutoService.Models;

namespace AutoService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AppointmentsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public AppointmentsController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Appointment>>> GetAppointments()
        {
            return await _context.Appointments
                                 .Include(a => a.Car)
                                 .Include(a => a.AssignedMechanic)
                                 .ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Appointment>> GetAppointment(int id)
        {
            var appointment = await _context.Appointments
                                            .Include(a => a.Car)
                                                .ThenInclude(c => c.Client)
                                            .Include(a => a.AssignedMechanic)
                                            .FirstOrDefaultAsync(a => a.Id == id);

            if (appointment == null)
            {
                return NotFound("Programarea nu a fost găsită.");
            }

            return appointment;
        }

        [HttpPost]
        public async Task<ActionResult<Appointment>> PostAppointment(Appointment appointment)
        {
            var carExists = await _context.Cars.AnyAsync(c => c.Id == appointment.CarId);
            if (!carExists)
            {
                return BadRequest("Mașina specificată nu există în sistem.");
            }

            if (appointment.AssignedMechanicId.HasValue)
            {
                var mechanicExists = await _context.Employees.AnyAsync(e => e.Id == appointment.AssignedMechanicId.Value);
                if (!mechanicExists)
                {
                    return BadRequest("Mecanicul specificat nu există.");
                }
            }

            _context.Appointments.Add(appointment);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetAppointment), new { id = appointment.Id }, appointment);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutAppointment(int id, Appointment appointment)
        {
            if (id != appointment.Id)
            {
                return BadRequest("ID-ul nu corespunde.");
            }

            _context.Entry(appointment).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AppointmentExists(id))
                {
                    return NotFound("Programarea nu a fost găsită.");
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAppointment(int id)
        {
            var appointment = await _context.Appointments.FindAsync(id);
            if (appointment == null)
            {
                return NotFound("Programarea nu a fost găsită.");
            }

            _context.Appointments.Remove(appointment);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool AppointmentExists(int id)
        {
            return _context.Appointments.Any(e => e.Id == id);
        }
    }
}