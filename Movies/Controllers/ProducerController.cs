using Microsoft.AspNetCore.Mvc;

namespace Movies.Controllers
{
    public class ProducerController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
