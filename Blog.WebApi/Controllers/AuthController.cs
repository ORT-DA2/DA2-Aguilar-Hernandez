using System.Security.Authentication;
using Blog.BusinessLogic.Filters;
using Blog.IBusinessLogic;
using Blog.WebApi.Controllers.DTOs.Auth;
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
                return Ok(new{token = token});
            }
            catch (InvalidCredentialException ex)
            {
                return Unauthorized(ex.Message);
            }
        }
        
        [HttpDelete]
        [Route("logout")]
        [ServiceFilter(typeof(AuthorizationFilter))]
        public IActionResult Logout([FromHeader] Guid token)
        {
            _sessionService.Logout(token);
            return Ok();
        }
        
    }
}
