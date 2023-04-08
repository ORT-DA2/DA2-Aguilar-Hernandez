using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Blog.BusinessLogic;
using Blog.BusinessLogic.Exceptions;
using Blog.Domain.Entities;
using Blog.WebApi.Controllers.DTOs;
using Microsoft.AspNetCore.Http;
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
        public IActionResult CreateUser()
        {
            return null;
        }
    }
}
