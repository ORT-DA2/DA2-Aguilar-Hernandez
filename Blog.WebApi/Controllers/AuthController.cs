using System.Security.Authentication;
using Blog.IBusinessLogic;
using Blog.Models.In.Auth;
using Microsoft.AspNetCore.Mvc;

namespace Blog.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private ISessionLogic _sessionService;
        
        public AuthController(ISessionLogic sessionService)
        {
            _sessionService = sessionService;
        }
        
        [HttpPost]
        [Route("login")]
        public IActionResult Login([FromBody] LoginDTO login)
        {
            try
            {
                var token = _sessionService.Login(login.Username, login.Password);
                return Ok(token);
            }
            catch (InvalidCredentialException ex)
            {
                return Unauthorized(ex.Message);
            }
        }
        
        [HttpDelete]
        [Route("logout")]
        public IActionResult Logout([FromHeader] Guid token)
        {
            _sessionService.Logout(token);
            return Ok();
        }
        
    }
}
