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
        [Route("ProdutorComMaiorIntevalo")]
        public IActionResult ProdutorComMaiorIntevalo()
        {

            var result = _mediator.Send(new ProducerMaxIntervalRequest());
            return Ok(result);
        }

        [HttpGet]
        [Route("ProdutorComMenorIntevalo")]
        public IActionResult ProdutorComMenorIntevalo()
        {

            var result = _mediator.Send(new ProducerMinIntervalRequest());
            return Ok(result);
        }

    }
}
