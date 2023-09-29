using Microsoft.AspNetCore.Mvc;
using Scheduler.Application.Interfaces;
using Scheduler.Application.ViewModels;

namespace Scheduler.Services.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientsController : ControllerBase
    {
        private readonly IClientService _clientsService;

        public ClientsController(IClientService clientsService)
        {
            _clientsService = clientsService;
        }

        // GET: api/Clients
        [HttpGet]
        public ActionResult<IEnumerable<ClientViewModel>> Get()
        {
            return Ok(_clientsService.Get());
        }

        // PUT: api/Clients/5
        [HttpPut("{id}")]
        public IActionResult Put(string id, ClientViewModel client)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                throw new ArgumentException($"'{nameof(id)}' não pode ser nulo nem espaço em branco.", nameof(id));
            }

            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            _clientsService.Update(client);

            return NoContent();
        }

        // POST: api/Clients
        [HttpPost]
        public ActionResult<ClientViewModel> Post(ClientViewModel client)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            _clientsService.Create(client);

            return CreatedAtAction("GetClient", new { id = client.Id }, client);
        }

        // DELETE: api/Clients/5
        [HttpDelete("{id}")]
        public ActionResult Delete(string id)
        {
            _clientsService.Delete(new Guid(id));

            return NoContent();
        }
    }
}