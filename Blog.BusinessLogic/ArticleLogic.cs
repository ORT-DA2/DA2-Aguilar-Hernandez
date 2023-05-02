using Blog.Domain.Entities;
using Blog.Domain.Enums;
using Blog.Domain.Exceptions;
using Blog.IBusinessLogic;
using Blog.IDataAccess;
using Microsoft.AspNetCore.Mvc;

namespace Blog.BusinessLogic;

public class ArticleLogic: IArticleLogic
{
    private readonly IRepository<Article> _repository;
    private static ISessionLogic _sessionLogic;
    public ArticleLogic(IRepository<Article> articleRepository, ISessionLogic sessionLogic)
    {
        _repository = articleRepository;
        _sessionLogic = sessionLogic;
    }

    public Article GetArticleById(Guid articleId)
    {
        Article? article = _repository.GetBy(a => a.Id == articleId);
        ValidateNull(article);
        return article;
    }

    public IEnumerable<Article> GetAllArticles()
    {
        IEnumerable<Article> articles = _repository.GetAll();
        ValidateListNull(articles);
        return articles;
    }
    
    public IEnumerable<Article> GetAllPublicArticles()
    {
        IEnumerable<Article> articles = _repository.GetPublicAll();
        ValidateListNull(articles);
        return articles;
    }
    
    public IEnumerable<Article> GetLastTen()
    {
        IEnumerable<Article> articles = _repository.GetLastTen();
        ValidateListNull(articles);
        return articles;
    }

    public IEnumerable<Article> GetAllUserArticles(string username, Guid authorization)
    {
        IEnumerable<Article> articles = _repository.GetUserArticles(username);
        ValidateListNull(articles);
        
        if (_sessionLogic.GetLoggedUser(authorization).Username != username)
        {
            throw new ArgumentException("You can´t update an article of other owner");
        }

        return articles;
    }

    public Article CreateArticle(Article article, Guid authorization)
    {
        article.DatePublished = DateTime.Now;
        article.DateLastModified = DateTime.Now;
        article.Owner = _sessionLogic.GetLoggedUser(authorization);
        _repository.Insert(article);
        _repository.Save();
        return article;
    }

    public IEnumerable<Article> GetArticleByText(string text)
    {
        IEnumerable<Article> articles = _repository.GetByText(text);
        ValidateListNull(articles);
        
        return articles;
    }

    public Article UpdateArticle(Guid id, Article article, Guid authorization)
    {
        Article? oldArticle = _repository.GetBy(a => a.Id == id);

        ValidateNull(oldArticle);
        ValidateUserOwner(oldArticle.Owner.Id, authorization);
        
        article.DateLastModified = DateTime.Now;
        article.DatePublished = oldArticle.DatePublished;
        article.Owner = oldArticle.Owner;
        oldArticle.UpdateAttributes(article);
        _repository.Update(oldArticle);
        _repository.Save();

        return oldArticle;
    }

    public void DeleteArticle(Guid articleId, Guid authorization)
    {
        Article? article = _repository.GetBy(a => a.Id == articleId);

        ValidateNull(article);
        ValidateUserOwner(article.Owner.Id, authorization);
        
        _repository.Delete(article);
        _repository.Save();
    }
    
    private static void ValidateNull(Article article)
    {
        if (article == null)
        {
            throw new NotFoundException("The article was not found");
        }
    }
    
    private static void ValidateListNull(IEnumerable<Article> articles)
    {
        if (articles == null || !articles.Any())
        {
            throw new NotFoundException("The are no articles");
        }
    }

    private static void ValidateUserOwner(Guid ownerId, Guid authorization)
    {
        if (_sessionLogic.GetLoggedUser(authorization).Id != ownerId)
        {
            throw new ArgumentException("You can´t delete an article of other owner");
        }
    }
    
}