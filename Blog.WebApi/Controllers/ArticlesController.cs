using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Blog.BusinessLogic.Exceptions;
using Blog.BusinessLogic.Filters;
using Blog.Domain.Entities;
using Blog.IBusinessLogic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Blog.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArticlesController : ControllerBase
    {
        private readonly IArticleLogic _articleLogic;

        public ArticlesController(IArticleLogic articleLogic)
        {
            _articleLogic = articleLogic;
        }

        [ServiceFilter(typeof(AuthorizationFilter))]
        [HttpGet("{id}")]
        public IActionResult GetArticleById([FromRoute] Guid id)
        {
            try
            {
                Article article = _articleLogic.GetArticleById(id);
                return Ok(article);
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
