using MediatR;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Mvc;
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

        //[HttpPost]
        //public IActionResult Create([FromBody] Producer command)
        //{
        //    _handler.Adicionar(command);
        //    return Ok(command);
        //}

        [HttpGet]
        public IActionResult Buscar([FromBody] ListarProducerRequest entrada)
        {

            var result = _mediator.Send(entrada);
            return Ok(result);
        }

         
    }
}
