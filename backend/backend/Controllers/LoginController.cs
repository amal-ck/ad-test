using backend.Data;
using backend.dto;
using Microsoft.AspNetCore.Mvc;
using backend.Models;

namespace backend.Controllers
{
    [ApiController]
    [Route("[Controller]")]
    public class LoginController: ControllerBase
    {
        private readonly AppDbContext _appDbContext;

        public LoginController(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] DtoLogin dtoMdlLogin)
        {
            return Ok();
        }

        [HttpPost("register")]
        public IActionResult Register([FromBody] DtoRegister dtoRegister)
        {
            try
            {
                MdlResponse mdlResponse = new MdlResponse();
                var existingUser = _appDbContext.Users.FirstOrDefault(user => user.Username == dtoRegister.UserName);
                if(existingUser != null)
                {
                    mdlResponse.Success = false;
                    mdlResponse.Message = "Registration Failed";
                    mdlResponse.ErrorMsg = "Username already taken";
                    return BadRequest(mdlResponse);
                }
                var user = new User
                {
                    Name = dtoRegister.Name,
                    Username = dtoRegister.UserName,
                    Password = dtoRegister.Password,
                };
                _appDbContext.Users.Add(user);
                _appDbContext.SaveChanges();

                mdlResponse.Success = true;
                mdlResponse.Message = "Registration Succesfull";
                return Ok(mdlResponse);
            }
            catch (Exception ex) {
                MdlResponse mdlResponse = new MdlResponse();
                mdlResponse.Success = false;
                mdlResponse.Message = "Registration Failed";
                mdlResponse.ErrorExMsg = Convert.ToString(ex.Message);
                return Ok(mdlResponse);
            }

        }
        
        [HttpGet("test")]
        public IActionResult Test()
        {
            return Ok("Test Success");
        }
    }
}
