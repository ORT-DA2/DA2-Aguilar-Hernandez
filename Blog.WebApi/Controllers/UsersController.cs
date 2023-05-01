using Blog.Domain.Entities;
using Blog.Domain.Enums;
using Blog.Domain.Exceptions;
using Blog.Filters;
using Blog.IBusinessLogic;
using Blog.Models.In;
using Blog.Models.Out;
using Microsoft.AspNetCore.Mvc;

namespace Blog.WebApi.Controllers;

[Route("api/users")]
[ApiController]
[ExceptionFilter]
public class UsersController : ControllerBase
{
    private readonly IUserLogic _userLogic;

    public UsersController(IUserLogic userLogic)
    {
        _userLogic = userLogic;
    }
    
    
    [ServiceFilter(typeof(AuthorizationFilter))]
    [HttpGet("{id}")]
    public IActionResult GetUserById([FromRoute] Guid id)
    {
         User user = _userLogic.GetUserById(id);
         return Ok(new UserDetailDTO(user));        
    }


    [ServiceFilter(typeof(AuthorizationFilter))]
    [HttpGet]
    public IActionResult GetAllUsers()
    {
         return Ok(_userLogic.GetAllUsers());
    }        
    
    [ServiceFilter(typeof(AuthorizationFilter))]
    [AuthenticationRoleFilter(Roles = new[] { Role.Admin })]
    [HttpPost]
    public IActionResult CreateUser([FromBody]CreateUserDTO userDto)
    {
        var user = userDto.ToEntity(userDto.Roles);
        var newUser = _userLogic.CreateUser(user);
        return Created($"api/users/{newUser.Id}",new UserDetailDTO(newUser));
    }

    [ServiceFilter(typeof(AuthorizationFilter))]
    [AuthenticationRoleFilter(Roles = new[] { Role.Admin, Role.Blogger })]
    [HttpPut("{id}")]
    public IActionResult UpdateUser([FromRoute] Guid id, [FromBody] CreateUserDTO userDto, [FromHeader] Guid Authorization)
    {
        User user = userDto.ToEntity(userDto.Roles);
        User newUser = _userLogic.UpdateUser(id, user, Authorization);
        return Created($"api/users/{newUser.Id}",new UserDetailDTO(newUser));
    }
        
        
    [ServiceFilter(typeof(AuthorizationFilter))]
    [AuthenticationRoleFilter(Roles = new[] { Role.Admin })]
    [HttpDelete("{id}")]
    public IActionResult DeleteUser([FromRoute] Guid id)
    {
        _userLogic.DeleteUser(id);
        return Ok($"User with the id {id} was deleted");
    }
        
    
}