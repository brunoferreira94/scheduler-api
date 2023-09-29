using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Scheduler.Application.Interfaces;
using Scheduler.Application.ViewModels;
using Scheduler.Domain.Entities;
using Scheduler.Domain.Interfaces.Repositories;
using System.Web.Http.ModelBinding;

namespace Scheduler.Services.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AppointmentsController : ControllerBase
    {
        private readonly IAppointmentService _appointmentsService;

        public AppointmentsController(IAppointmentService appointmentsService)
        {
            _appointmentsService = appointmentsService;
        }

        // GET: api/Appointments
        [HttpGet]
        public ActionResult<IEnumerable<AppointmentViewModel>> Get()
        {
            return Ok(_appointmentsService.Get().OrderBy(appointment => appointment.ScheduledDate));
        }

        // PUT: api/Appointments/5
        [HttpPut("{id}")]
        public IActionResult Put(string id, AppointmentViewModel appointment)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                throw new ArgumentException($"'{nameof(id)}' não pode ser nulo nem espaço em branco.", nameof(id));
            }

            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            return Ok(_appointmentsService.Update(appointment));
        }

        // POST: api/Appointments
        [HttpPost]
        public ActionResult<AppointmentViewModel> Post(AppointmentViewModel appointment)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            return Created("Appointments", _appointmentsService.Create(appointment));
        }

        // DELETE: api/Appointments/5
        [HttpDelete("{id}")]
        public ActionResult Delete(string id)
        {
            return Ok(_appointmentsService.Delete(new Guid(id)));
        }
    }
}