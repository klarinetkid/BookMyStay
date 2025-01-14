using Microsoft.AspNetCore.Mvc;

namespace BookMyStay.Controllers
{
    public class HomeController : Controller
    {
        // GET /
        public IActionResult Index()
        {
            return Ok();
        }
    }
}
