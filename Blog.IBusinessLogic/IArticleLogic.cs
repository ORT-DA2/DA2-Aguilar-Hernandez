using Blog.Domain.Entities;

namespace Blog.IBusinessLogic;

public interface IArticleLogic
{
    public Article GetArticleById(Guid articleId);
    public IEnumerable<Article> GetAllArticles();
    public Article CreateArticle(Article article, Guid authorization);
    public IEnumerable<Article> GetArticleByText(string text);
    public Article UpdateArticle(Guid id, Article article, Guid authorization);
    public void DeleteArticle(Guid articleId, Guid authorization);
    public IEnumerable<Article> GetLastTen();
}