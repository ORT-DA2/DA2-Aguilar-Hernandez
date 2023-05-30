using Blog.Domain.Entities;
using Microsoft.AspNetCore.Http;

namespace Blog.IBusinessLogic;

public interface IArticleLogic
{
    public Article GetArticleById(Guid articleId);
    public IEnumerable<Article> GetAllArticles();
    public IEnumerable<Article> GetAllPublicArticles();
    public Article CreateArticle(Article article, Guid authorization);
    public IEnumerable<Article> GetArticleByText(string text);
    public Article UpdateArticle(Guid id, Article article, Guid authorization);
    public Article ApproveArticle(Guid id, Article article);
    public void DeleteArticle(Guid articleId, Guid authorization);
    public IEnumerable<Article> GetLastTen();
    public IEnumerable<Article> GetAllUserArticles(string username, Guid auth);
    string SaveImage(string imageFile);
}