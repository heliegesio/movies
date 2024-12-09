using Microsoft.AspNetCore.Mvc;
using Movies.Domain.Models;
using Movies.Infrastructure.Repositories;

namespace Movies.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProducerController : ControllerBase
    {
        private readonly IProducerRepository _handler;

        public ProducerController(IProducerRepository handler)
        {
            _handler = handler;
        }

        [HttpPost]
        public IActionResult Create([FromBody] Producer command)
        {
            _handler.Adicionar(command);
            return Ok(command);
        }

        [HttpGet]
        public IActionResult Buscar()
        {
            
            var resl = _handler.Obter();
            return Ok(resl);
        }

         
    }
}
