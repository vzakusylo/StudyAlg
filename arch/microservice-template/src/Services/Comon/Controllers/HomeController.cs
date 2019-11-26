using Microsoft.AspNetCore.Mvc;

namespace Usavc.Microservices.Appointment.Controllers
{
    [Route("")]
    public class HomeController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get() => Ok("Usavc Appointment Service");

        [HttpGet("ping")]
        public IActionResult Ping() => Ok();
    }
}