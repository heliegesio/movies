using MediatR;
using Microsoft.AspNetCore.Mvc;
using Movies.Application.Commands.ProducerCommands.Request;
using Movies.Application.Queries.ProducerQuerys.Request;

namespace Movies.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProducerController : ControllerBase
    {
        private readonly IMediator _mediator;


        public ProducerController(IMediator mediator)
        {

            _mediator = mediator;
        }

        [HttpPost]
        public IActionResult Create([FromBody] CreateProducerRequest command)
        {
            var result = _mediator.Send(command);
            return Ok(result);
        }

        [HttpGet]
        public IActionResult Buscar([FromQuery] ListarProducerRequest command)
        {

            var result = _mediator.Send(command);
            return Ok(result);
        }

        [HttpGet]
        [Route("ProducerWithLongestInterval")]
        public IActionResult ProducerWithLongestInterval([FromQuery] ProducerTestIntervalRequest command)
        {

            var result = _mediator.Send(command);
            return Ok(result);
        }

    }
}
