using Blog.BusinessLogic.Exceptions;
using Blog.Domain.Entities;
using Blog.IBusinessLogic;
using Blog.WebApi.Controllers.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace Blog.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserLogic _userLogic;

        public UsersController(IUserLogic userLogic)
        {
            _userLogic = userLogic;
        }

        [HttpGet("{id}")]
        public IActionResult GetUserById([FromRoute] Guid id)
        {
            try
            {
                User user = _userLogic.GetUserById(id);
                return Ok(new UserDetailDTO(user));
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpGet]
        public IActionResult GetAllUsers()
        {
            try
            {
                return Ok(_userLogic.GetAllUsers());
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPost]
        public IActionResult CreateUser([FromBody]CreateUserDTO userDto)
        {
            try
            {
                User user = userDto.ToEntity(userDto.Roles);
                User newUser = _userLogic.CreateUser(user);
                return Created($"api/users/{newUser.Id}",new UserDetailDTO(newUser));
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public IActionResult UpdateUser([FromRoute] Guid id, [FromBody] CreateUserDTO userDto, [FromHeader] Guid Authorization)
        {
            try
            {
                User user = userDto.ToEntity(userDto.Roles);
                User newUser = _userLogic.UpdateUser(id, user, Authorization);
                return Created($"api/users/{newUser.Id}",new UserDetailDTO(newUser));
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteUser([FromRoute] Guid id)
        {
            try
            {
                _userLogic.DeleteUser(id);
                return Ok($"User with the id {id} was deleted");
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
