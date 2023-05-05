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

        [HttpGet("{id}")]
        [ServiceFilter(typeof(AuthorizationFilter))]
        [AuthenticationRoleFilter(Roles = new[] { Role.Blogger })]
        public IActionResult GetArticleById([FromRoute] Guid id)
        {
            Article article = _articleLogic.GetArticleById(id);
            return Ok(new ArticleDetailDTO(article));
        }

        [HttpGet("public")]
        [ServiceFilter(typeof(AuthorizationFilter))]
        [AuthenticationRoleFilter(Roles = new[] { Role.Blogger })]
        public IActionResult GetAllPublicArticles()
        {
            IEnumerable<Article> articles = _articleLogic.GetAllPublicArticles();
            List<ArticleDetailDTO> articlesDTO = articles.Select(article => new ArticleDetailDTO(article)).ToList();
            return Ok(articlesDTO);

        }
        
        [HttpGet]
        [ServiceFilter(typeof(AuthorizationFilter))]
        [AuthenticationRoleFilter(Roles = new[] { Role.Blogger })]
        public IActionResult GetAllUserArticles([FromQuery] string username, [FromHeader] Guid authorization)
        {
            IEnumerable<Article> articles = _articleLogic.GetAllUserArticles(username, authorization);
            List<ArticleDetailDTO> articlesDTO = articles.Select(article => new ArticleDetailDTO(article)).ToList();
            return Ok(articlesDTO);

        }

        [HttpGet("search")]
        //[ServiceFilter(typeof(AuthorizationFilter))]
        //[AuthenticationRoleFilter(Roles = new[] { Role.Blogger })]
        public IActionResult GetArticleByText([FromQuery] string text)
        {
            IEnumerable<Article> articles = _articleLogic.GetArticleByText(text);
            List<ArticleDetailDTO> articlesDTO = articles.Select(article => new ArticleDetailDTO(article)).ToList();
            return Ok(articlesDTO);
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

        [HttpGet("last-articles")]
        public IActionResult GetLastTen()
        {
            return Ok(_articleLogic.GetLastTen());
        }
    }
}