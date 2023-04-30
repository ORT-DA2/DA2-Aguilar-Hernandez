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
    private readonly ISessionLogic _sessionLogic;
    public ArticleLogic(IRepository<Article> articleRepository, ISessionLogic sessionLogic)
    {
        _repository = articleRepository;
        _sessionLogic = sessionLogic;
    }

    public Article GetArticleById(Guid articleId)
    {
        var article = _repository.GetById(a => a.Id == articleId);
        ValidateNull(article);
        return article;
    }

    public IEnumerable<Article> GetAllArticles()
    {
        var articles = _repository.GetAll();
        ValidateListNull(articles);
        return articles;
    }
    
    public IEnumerable<Article> GetAllPublicArticles()
    {
        var articles = _repository.GetPublicAll();
        ValidateListNull(articles);
        return articles;
    }
    
    public IEnumerable<Article> GetLastTen()
    {
        var articles = _repository.GetLastTen();
        ValidateListNull(articles);
        return articles;
    }

    public IEnumerable<Article> GetAllPrivateArticles(string username, Guid authorization)
    {
        var articles = _repository.GetPrivateAll(username);
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
        var articles = _repository.GetByText(text);
        ValidateListNull(articles);
        
        return articles;
    }

    public Article UpdateArticle(Guid id, Article article, Guid authorization)
    {
        var oldArticle = _repository.GetById(a => a.Id == id);

        ValidateNull(oldArticle);
        

        if (_sessionLogic.GetLoggedUser(authorization).Roles.All(ur => ur.Role != Role.Blogger ))
        {
            if (_sessionLogic.GetLoggedUser(authorization).Id != oldArticle.Owner.Id)
            {
                throw new ArgumentException("You can´t update an article of other owner");
            }
        }
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
        var article = _repository.GetById(a => a.Id == articleId);

        ValidateNull(article);

        if (_sessionLogic.GetLoggedUser(authorization).Roles.All(ur => ur.Role != Role.Blogger ))
        {
            if (_sessionLogic.GetLoggedUser(authorization).Id != article.Owner.Id)
            {
                throw new ArgumentException("You can´t delete an article of other owner");
            }
        }
        
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
    
}