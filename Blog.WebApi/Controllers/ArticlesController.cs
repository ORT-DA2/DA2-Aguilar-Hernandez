using Blog.Domain.Entities;
using Blog.Domain.Exceptions;
using Blog.Filters;
using Blog.IBusinessLogic;
using Blog.Models.In;
using Blog.Models.Out;
using Microsoft.AspNetCore.Mvc;

namespace Blog.WebApi.Controllers
{
    [Route("api/articles")]
    [ApiController]
    [ExceptionFilter]
    public class ArticlesController : ControllerBase
    {
        private readonly IArticleLogic _articleLogic;

        public ArticlesController(IArticleLogic articleLogic)
        {
            _articleLogic = articleLogic;
        }

        [HttpGet("id/{id}")]
        public IActionResult GetArticleById([FromRoute] Guid id)
        {
            Article article = _articleLogic.GetArticleById(id);
            return Ok(new ArticleDetailDTO(article));
        }

        [HttpGet]
        public IActionResult GetAllArticles()
        { 
            return Ok(_articleLogic.GetAllArticles());
        }
        
        [HttpGet("public")]
        public IActionResult GetAllPublicArticles()
        {
            return Ok(_articleLogic.GetAllPublicArticles());

        }
        
        [HttpGet("{username}")]
        public IActionResult GetAllPrivateArticles([FromRoute] string username, [FromHeader] Guid authorization)
        {
            return Ok(_articleLogic.GetAllPrivateArticles(username, authorization));

        }

        [HttpGet("search")]
        public IActionResult GetArticleByText([FromQuery] string text)
        {
            return Ok(_articleLogic.GetArticleByText(text));
        }

        [HttpPost]
        public IActionResult CreateArticle([FromForm] CreateArticleDTO articleDto, [FromHeader] Guid Authorization)
        {
            try
            {
                Article article = articleDto.ToEntity();
                Article newArticle = _articleLogic.CreateArticle(article, Authorization);
                return Created($"api/articles/{article.Id}", new ArticleDetailDTO(newArticle));
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpPut("{id}")]
        public IActionResult UpdateArticle([FromRoute] Guid id, [FromForm] CreateArticleDTO articleDto,
            [FromHeader] Guid Authorization)
        {
            try
            {
                Article article = articleDto.ToEntity();
                Article newArticle = _articleLogic.UpdateArticle(id, article, Authorization);
                return Created($"api/articles/{newArticle.Id}", new ArticleDetailDTO(newArticle));
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteArticle([FromRoute] Guid id, [FromHeader] Guid Authorization)
        {
            try
            {
                _articleLogic.DeleteArticle(id, Authorization);
                return Ok($"Article with the id {id} was deleted");
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);

            }
        }

        [HttpGet("LastTenArticles")]
        public IActionResult GetLastTen()
        {
            return Ok(_articleLogic.GetLastTen());
        }
    }
}