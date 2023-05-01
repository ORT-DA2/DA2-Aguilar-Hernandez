using Blog.Filters;
using Blog.IBusinessLogic;
using Blog.Models.In;
using Microsoft.AspNetCore.Mvc;

namespace Blog.WebApi.Controllers;

[Route("api/auth")]
[ApiController]
[ExceptionFilter]
public class AuthController : ControllerBase
{
    private ISessionLogic _sessionService;
    private IUserLogic _userLogic;

    public AuthController(ISessionLogic sessionService, IUserLogic userLogic)
    {
        _sessionService = sessionService;
        _userLogic = userLogic;
    }
    
    [HttpPost]
    [Route("register")]
    public IActionResult Register([FromBody] RegisterDto register)
    {
        var user = register.ToEntity();
        var newUser = _userLogic.CreateUser(user);
        return Ok(newUser);
    }
        
    [HttpPost]
    [Route("login")]
    public IActionResult Login([FromBody] LoginDto login)
    {
        var token = _sessionService.Login(login.Username, login.Password);
        return Ok(new{token = token});
    }
        
    [HttpDelete]
    [Route("logout")]
    [ServiceFilter(typeof(AuthorizationFilter))]
    public IActionResult Logout([FromHeader] Guid Authorization)
    {
        _sessionService.Logout(Authorization);
        return Ok("Logout successfuly");
    }
        
}