using Blog.Domain.Entities;
using Blog.Domain.Exceptions;
using Blog.IBusinessLogic;
using Blog.Models.In;
using Blog.Models.Out;
using Microsoft.AspNetCore.Mvc;

namespace Blog.WebApi.Controllers
{
    [Route("api/articles")]
    [ApiController]
    public class ArticlesController : ControllerBase
    {
        private readonly IArticleLogic _articleLogic;

        public ArticlesController(IArticleLogic articleLogic)
        {
            _articleLogic = articleLogic;
        }

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
        public IActionResult GetAllArticles()
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

        [HttpGet("search")]
        public IActionResult GetArticleByText([FromQuery] string text)
        {
            try
            {
                return Ok(_articleLogic.GetArticleByText(text));
            }
            catch (NotFoundException ex)
            {
                return NotFound("There are no articles with that text.");
            }
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
            try
            {
                return Ok(_articleLogic.GetLastTen());
            }
            catch (NotFoundException ex)
            {
                return NotFound("There are no articles.");
            }
        }
    }
}
