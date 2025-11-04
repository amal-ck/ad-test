using backend.Data;
using backend.dto;
using Microsoft.AspNetCore.Mvc;
using backend.Models;
using Microsoft.AspNetCore.Identity;
using backend.Processors;
using Microsoft.AspNetCore.Authorization;

namespace backend.Controllers
{
    [ApiController]
    [Route("[Controller]")]
    public class LoginController : ControllerBase
    {
        private readonly AppDbContext _appDbContext;
        private readonly IConfiguration _conf;

        public LoginController(AppDbContext appDbContext, IConfiguration configuration)
        {
            _appDbContext = appDbContext;
            _conf = configuration;
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] DtoLogin dtoLogin)
        {
            try
            {
                LoginProcess login = new LoginProcess();
                MdlResponse mdlResponse = new MdlResponse();
                string token = "";
                (mdlResponse, token) = login.FnLogin(dtoLogin, _appDbContext, _conf);

                Response.Cookies.Append("access_token", token, new CookieOptions
                {
                    HttpOnly = true,
                    Secure = true,
                    SameSite = SameSiteMode.None,
                    Expires = DateTime.UtcNow.AddMinutes(Convert.ToDouble(_conf["Jwt:ExpiryMinutes"]))
                });

                return Ok(mdlResponse);
            }
            catch (UnauthorizedAccessException ex)
            {
                MdlResponse mdlResponse = new MdlResponse();
                mdlResponse.Success = false;
                mdlResponse.Message = "Login Failed";
                mdlResponse.ErrorMsg = ex.Message;
                return Unauthorized(mdlResponse);
            }
            catch (Exception ex)
            {
                MdlResponse mdlResponse = new MdlResponse();
                mdlResponse.Success = false;
                mdlResponse.Message = "Login Failed";
                mdlResponse.ErrorExMsg = Convert.ToString(ex.Message);
                return StatusCode(500, mdlResponse);
            }
        }

        [HttpPost("register")]
        public IActionResult Register([FromBody] DtoRegister dtoRegister)
        {
            try
            {
                LoginProcess login = new LoginProcess();
                MdlResponse mdlResponse = new MdlResponse();
                string token = "";
                (mdlResponse, token )= login.FnRegister(dtoRegister, _appDbContext, _conf);
                Response.Cookies.Append("access_token", token, new CookieOptions
                {
                    HttpOnly = true,
                    Secure = true,
                    SameSite = SameSiteMode.None,
                    Expires = DateTime.UtcNow.AddMinutes(Convert.ToDouble(_conf["Jwt:ExpiryMinutes"]))
                });
                return Ok(mdlResponse);

            }
            catch (BadHttpRequestException ex)
            {
                MdlResponse mdlResponse = new MdlResponse();
                mdlResponse.Success = false;
                mdlResponse.Message = "Registration Failed";
                mdlResponse.ErrorMsg = ex.Message;
                return BadRequest(mdlResponse);
            }
            catch (Exception ex)
            {
                MdlResponse mdlResponse = new MdlResponse();
                mdlResponse.Success = false;
                mdlResponse.Message = "Registration Failed";
                mdlResponse.ErrorExMsg = Convert.ToString(ex.Message);
                return StatusCode(500, mdlResponse);
            }

        }

        [Authorize]
        [HttpGet("userData")]
        public IActionResult GetUserData()
        {
            var userId = User.FindFirst("UserId")?.Value;
            var username = User.Identity?.Name;
            return Ok(new { UserId = userId, Username = username });
        }

        [HttpGet("test")]
        public IActionResult Test()
        {
            return Ok("Test Success");
        }

        [Authorize]
        [HttpGet("checkLogin")]
        public IActionResult CheckLogin()
        {
            // If user is authorized, this endpoint will only be reached if token (cookie) is valid
            return Ok(new { isLoggedIn = true });
        }
    }
}
