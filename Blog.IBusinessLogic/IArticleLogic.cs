using Blog.Domain.Entities;

namespace Blog.IBusinessLogic;

public interface IArticleLogic
{
    public Article GetArticleById(Guid articleTestId);
    public IEnumerable<Article> GetAllArticles();
    public Article CreateArticle(Article article);
}