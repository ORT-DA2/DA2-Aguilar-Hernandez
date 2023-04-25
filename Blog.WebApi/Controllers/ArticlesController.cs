using Blog.BusinessLogic.Exceptions;
using Blog.Domain.Entities;
using Blog.Filters;
using Blog.IBusinessLogic;
using Blog.Models.In.Article;
using Blog.Models.Out.Article;
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
                return Ok(new ArticleDetailDTO(article));
            }
            catch (NotFoundException ex)
            {
                return NotFound("There are no articles with the id");
            }
        }

        [HttpGet]
        public IActionResult GetAllUsers()
        {
            try
            {
                return Ok(_articleLogic.GetAllArticles());
            }
            catch (NotFoundException ex)
            {
                return NotFound("There are no articles.");
            }
            
        }
        
        [HttpPost]
        public IActionResult CreateUser([FromBody]CreateArticleDTO articleDto)
        {
            try
            {
                Article article = articleDto.ToEntity();
                Article newArticle = _articleLogic.CreateArticle(article);
                return Created($"api/articles/{article.Id}", new ArticleDetailDTO(newArticle));
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            
        }

        public IActionResult GetArticleByText(string text)
        {
            return Ok(_articleLogic.GetArticleByText(text));
        }
    }
}
