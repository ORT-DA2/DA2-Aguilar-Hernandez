using Blog.Domain.Entities;
using Blog.Domain.Enums;
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
        [ServiceFilter(typeof(AuthorizationFilter))]
        [AuthenticationRoleFilter(Roles = new[] { Role.Blogger })]
        public IActionResult GetArticleById([FromRoute] Guid id)
        {
            Article article = _articleLogic.GetArticleById(id);
            return Ok(new ArticleDetailDTO(article));
        }

        [HttpGet]
        [ServiceFilter(typeof(AuthorizationFilter))]
        [AuthenticationRoleFilter(Roles = new[] { Role.Blogger })]
        public IActionResult GetAllArticles()
        { 
            return Ok(_articleLogic.GetAllArticles());
        }
        
        [HttpGet("public")]
        [ServiceFilter(typeof(AuthorizationFilter))]
        [AuthenticationRoleFilter(Roles = new[] { Role.Blogger })]
        public IActionResult GetAllPublicArticles()
        {
            return Ok(_articleLogic.GetAllPublicArticles());

        }
        
        [HttpGet("{username}")]
        [ServiceFilter(typeof(AuthorizationFilter))]
        [AuthenticationRoleFilter(Roles = new[] { Role.Blogger })]
        public IActionResult GetAllPrivateArticles([FromRoute] string username, [FromHeader] Guid authorization)
        {
            return Ok(_articleLogic.GetAllPrivateArticles(username, authorization));

        }

        [HttpGet("search")]
        [ServiceFilter(typeof(AuthorizationFilter))]
        [AuthenticationRoleFilter(Roles = new[] { Role.Blogger })]
        public IActionResult GetArticleByText([FromQuery] string text)
        {
            return Ok(_articleLogic.GetArticleByText(text));
        }

        [HttpPost]
        [ServiceFilter(typeof(AuthorizationFilter))]
        [AuthenticationRoleFilter(Roles = new[] { Role.Blogger })]
        public IActionResult CreateArticle([FromBody] CreateArticleDTO articleDto, [FromHeader] Guid Authorization)
        {
            Article article = articleDto.ToEntity();
            Article newArticle = _articleLogic.CreateArticle(article, Authorization);
            return Created($"api/articles/{article.Id}", new ArticleDetailDTO(newArticle));

        }

        [HttpPut("{id}")]
        [ServiceFilter(typeof(AuthorizationFilter))]
        [AuthenticationRoleFilter(Roles = new[] { Role.Blogger })]
        public IActionResult UpdateArticle([FromRoute] Guid id, [FromBody] CreateArticleDTO articleDto,
            [FromHeader] Guid Authorization)
        {
            Article article = articleDto.ToEntity();
            Article newArticle = _articleLogic.UpdateArticle(id, article, Authorization);
            return Created($"api/articles/{newArticle.Id}", new ArticleDetailDTO(newArticle));
        }

        [HttpDelete("{id}")]
        [ServiceFilter(typeof(AuthorizationFilter))]
        [AuthenticationRoleFilter(Roles = new[] { Role.Blogger })]
        public IActionResult DeleteArticle([FromRoute] Guid id, [FromHeader] Guid Authorization)
        {
            _articleLogic.DeleteArticle(id, Authorization);
            return Ok($"Article with the id {id} was deleted");
        }

        [HttpGet("LastTenArticles")]
        public IActionResult GetLastTen()
        {
            return Ok(_articleLogic.GetLastTen());
        }
    }
}
