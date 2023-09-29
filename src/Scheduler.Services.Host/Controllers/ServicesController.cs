using Microsoft.AspNetCore.Mvc;
using Scheduler.Application.Interfaces;
using Scheduler.Application.ViewModels;

namespace Scheduler.Services.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ServicesController : ControllerBase
    {
        private readonly IServiceService _servicesService;

        public ServicesController(IServiceService servicesService)
        {
            _servicesService = servicesService;
        }

        // GET: api/Services
        [HttpGet]
        public ActionResult<IEnumerable<ServiceViewModel>> Get()
        {
            return Ok(_servicesService.Get());
        }

        // PUT: api/Services/5
        [HttpPut("{id}")]
        public IActionResult Put(string id, ServiceViewModel service)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                throw new ArgumentException($"'{nameof(id)}' não pode ser nulo nem espaço em branco.", nameof(id));
            }

            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            _servicesService.Update(service);

            return NoContent();
        }

        // POST: api/Services
        [HttpPost]
        public ActionResult<ServiceViewModel> Post(ServiceViewModel service)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            _servicesService.Create(service);

            return CreatedAtAction("GetService", new { id = service.Id }, service);
        }

        // DELETE: api/Services/5
        [HttpDelete("{id}")]
        public ActionResult Delete(string id)
        {
            _servicesService.Delete(new Guid(id));

            return NoContent();
        }
    }
}