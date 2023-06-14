using Blog.Domain.Entities;
using Blog.Domain.Enums;
using Blog.Filters;
using Blog.IBusinessLogic;
using Blog.ImporterInterface;
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
        private readonly IImporterLogic _importerLogic;
        
        public ArticlesController(IArticleLogic articleLogic, IImporterLogic importerLogic)
        {
            _articleLogic = articleLogic;
            _importerLogic = importerLogic;
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
        [ServiceFilter(typeof(AuthorizationFilter))]
        [AuthenticationRoleFilter(Roles = new[] { Role.Blogger })]
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
            var imagePath = _articleLogic.SaveImage(articleDto.Image);

            Article article = articleDto.ToEntity();
            article.Image = imagePath;

            if (!string.IsNullOrEmpty(articleDto.Image2))
            {
                var imagePath2 = _articleLogic.SaveImage(articleDto.Image2);
                article.Image2 = imagePath2;
            }
            
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
        
        [HttpPut("approve/{id}")]
        [ServiceFilter(typeof(AuthorizationFilter))]
        [AuthenticationRoleFilter(Roles = new[] { Role.Admin })]
        public IActionResult ApproveArticle([FromRoute] Guid id,[FromBody] CreateArticleDTO articleDto)
        {
            Article article = articleDto.ToEntity();
            Article newArticle = _articleLogic.ApproveArticle(id, article);
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
            IEnumerable<Article> articles = _articleLogic.GetLastTen();
            List<ArticleDetailDTO> articlesDTO = articles.Select(article => new ArticleDetailDTO(article)).ToList();
            return Ok(articlesDTO);
        }
        
        [HttpGet("importers")]
        public IActionResult GetImporters()
        {
            List<IImporter> retrievedImporters = _importerLogic.GetAllImporters();
            List<ImportOutModel> models = retrievedImporters.Select(i => new ImportOutModel(i)).ToList();
            return Ok(models);
        }
        
        [HttpPost("import")] 
        public IActionResult ImportArticles([FromBody] ImportInModel importModel, [FromHeader] Guid Authorization)
        {
            List<ArticleDetailDTO> importedArticles = _importerLogic
                .ImportArticles(importModel.ImporterName, importModel.Parameters.Select(p => p.ToEntity()).ToList(), Authorization)
                .Select(article => new ArticleDetailDTO(article)).ToList();
            return Ok(importedArticles);
        }
        
    }
}