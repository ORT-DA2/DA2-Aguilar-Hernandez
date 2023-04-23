using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Blog.BusinessLogic.Filters;
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
            throw new NotImplementedException();
        }
    }
}
