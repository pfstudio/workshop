using Microsoft.AspNetCore.Mvc;

namespace workshop.Controllers
{
    public class RecordController: Controller
    {
        [HttpGet]
        public IActionResult Query()
        {
            return Ok("Query");
        }

        [HttpPost]
        public IActionResult Sign()
        {
            return Ok("SignOut");
        }

        [HttpPost]
        public IActionResult SignOut()
        {
            return Ok("SignOut");
        }
    }
}