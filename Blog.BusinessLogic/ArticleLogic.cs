using Blog.DataAccess;
using Blog.Domain.Entities;
using Blog.IBusinessLogic;
using Blog.IDataAccess;

namespace Blog.BusinessLogic;

public class ArticleLogic: IArticleLogic
{
    private readonly IRepository<Article> _repository;
    public ArticleLogic(IRepository<Article> articleRepository)
    {
        _repository = articleRepository;
    }

    public Article GetArticleById(Guid articleId)
    {
        return _repository.GetById(a => a.Id == articleId);
    }

    public IEnumerable<Article> GetAllArticles()
    {
        return _repository.GetAll();
    }
    
    public IEnumerable<Article> GetLastTen()
    {
        throw new NotImplementedException();
    }

    public Article CreateArticle(Article article)
    {
        _repository.Insert(article);
        _repository.Save();
        return article;
    }

    public IEnumerable<Article> GetArticleByText(string text)
    {
        return _repository.GetByText(text);
    }

    public Article UpdateArticle(Guid id, Article article, Guid authorization)
    {
        throw new NotImplementedException();
    }

    public void DeleteArticle(Guid articleId)
    {
        throw new NotImplementedException();
    }
    
}