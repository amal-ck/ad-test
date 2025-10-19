using backend.dto;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers
{
    [ApiController]
    [Route("[Controller]")]
    public class LoginController: ControllerBase
    {
        [HttpPost("login")]
        public IActionResult Login([FromBody] MdlLogin objMdlLogin)
        {
            return Ok();
        }

        [HttpPost("register")]
        public IActionResult Register([FromBody] MdlLogin objMdlLogin)
        {
            return Ok();
        }
        
        [HttpGet("test")]
        public IActionResult Test()
        {
            return Ok("Test Success");
        }
    }
}
