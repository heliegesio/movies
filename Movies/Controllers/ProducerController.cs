using MediatR;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Mvc;
using Movies.Application.Commands.ProducerCommands.Request;
using Movies.Application.Queries.ProducerQuerys.Request;
using Movies.Domain.Models;
using Movies.Infrastructure.Repositories;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

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

         
    }
}
