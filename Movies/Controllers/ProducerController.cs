using Microsoft.AspNetCore.Mvc;
using Movies.Application.Commands;
using Movies.Application.Handlers;
using Movies.Application.Queries;

namespace Movies.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProducerController : ControllerBase
    {
        private readonly ProducerHandler _handler;

        public ProducerController(ProducerHandler handler)
        {
            _handler = handler;
        }

        [HttpPost]
        public IActionResult Create([FromBody] CreateProducerCommand command)
        {
            _handler.Handle(command);
            return CreatedAtAction(nameof(Get), new { id = command.Producer }, command);
        }

        [HttpGet]
        public IActionResult Get([FromQuery] string type)
        {
            var query = new GetProducersQuery { Type = type };
            var producers = _handler.Handle(query);
            return Ok(producers);
        }
    }
}
