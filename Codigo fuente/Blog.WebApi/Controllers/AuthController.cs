
using Blog.Domain.Entities;
using Blog.Domain.Enums;
using Blog.Filters;
using Blog.IBusinessLogic;
using Blog.Models.In;
using Blog.Models.Out;
using Microsoft.AspNetCore.Mvc;

namespace Blog.WebApi.Controllers;

[Route("api/auth")]
[ApiController]
[ExceptionFilter]
public class AuthController : ControllerBase
{
    private ISessionLogic _sessionService;
    private INotificationLogic _notificationLogic;
    private IUserLogic _userLogic;

    public AuthController(ISessionLogic sessionService, IUserLogic userLogic, INotificationLogic notificationLogic)
    {
        _sessionService = sessionService;
        _userLogic = userLogic;
        _notificationLogic = notificationLogic;
    }
    
    [HttpPost]
    [Route("register")]
    public IActionResult Register([FromBody] RegisterDto register)
    {
        User user = register.ToEntity();
        User newUser = _userLogic.CreateUser(user);
        return Ok(newUser);
    }
        
    [HttpPost]
    [Route("login")]
    public IActionResult Login([FromBody] LoginDto login)
    {
        Guid token = _sessionService.Login(login.Username, login.Password);

        User? loggedUser = _sessionService.GetLoggedUser(token);
        IEnumerable<Notification> notifications = _notificationLogic.GetUnreadNotificationsByUser(loggedUser);
        return Ok(new{token = token, notifications = notifications, user = new UserDetailDTO(loggedUser)});
        
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