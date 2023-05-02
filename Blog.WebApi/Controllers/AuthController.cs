using System.Security.Authentication;
using Blog.Domain.Entities;
using Blog.Filters;
using Blog.IBusinessLogic;
using Blog.Models.In.Auth;
using Microsoft.AspNetCore.Mvc;

namespace Blog.WebApi.Controllers;

[Route("api/auth")]
[ApiController]
public class AuthController : ControllerBase
{
    private ISessionLogic _sessionService;
    private INotificationLogic _notificationLogic;
    public AuthController(ISessionLogic sessionService, INotificationLogic notificationLogic)
    {
        _sessionService = sessionService;
        _notificationLogic = notificationLogic;
    }
        
    [HttpPost]
    [Route("login")]
    public IActionResult Login([FromBody] LoginDTO login)
    {
        try
        {
            Guid token = _sessionService.Login(login.Username, login.Password);
            User? loggedUser = _sessionService.GetLoggedUser(token);
            IEnumerable<Notification> notifications = _notificationLogic.GetUnreadNotificationsByUser(loggedUser);
            return Ok(new{token = token, notifications = notifications});
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