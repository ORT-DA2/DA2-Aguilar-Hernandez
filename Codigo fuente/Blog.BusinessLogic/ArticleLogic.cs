using Blog.Domain.Entities;
using Blog.Domain.Exceptions;
using Blog.IBusinessLogic;
using Blog.IDataAccess;
using Microsoft.AspNetCore.Hosting;

namespace Blog.BusinessLogic;

public class ArticleLogic: IArticleLogic
{
    private readonly IRepository<Article> _repository;
    private static ISessionLogic _sessionLogic;
    private static IOffensiveWordLogic _offensiveWordLogic;
    private readonly IWebHostEnvironment _hostEnvironment;

    public ArticleLogic(IRepository<Article> articleRepository, ISessionLogic sessionLogic, IOffensiveWordLogic offensiveWordLogic, IWebHostEnvironment hostEnvironment)
    {
        _repository = articleRepository;
        _sessionLogic = sessionLogic;
        _offensiveWordLogic = offensiveWordLogic;
        _hostEnvironment = hostEnvironment;
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
            throw new ArgumentException("You can´t see all articles of other owner");
        }

        return articles;
    }

    public string SaveImage(string image)
    {
        byte[] imageBytes = Convert.FromBase64String(image);
        var imageFolderPath = Path.Combine(_hostEnvironment.WebRootPath, "images");

        if (!Directory.Exists(imageFolderPath))
        {
            Directory.CreateDirectory(imageFolderPath);
        }

        var imageName = $"{Guid.NewGuid().ToString()}.jpg";
        var fullImagePath = Path.Combine(imageFolderPath, imageName);

        System.IO.File.WriteAllBytesAsync(fullImagePath, imageBytes);

        return $"images/{imageName}";
    }

    public Article CreateArticle(Article article, Guid authorization)
    {
        ValidateNull(article);
        article.Owner = _sessionLogic.GetLoggedUser(authorization);
        article.DatePublished = DateTime.Now;
        article.DateLastModified = DateTime.Now;
        
        _offensiveWordLogic.ValidateArticleOffensiveWords(article);
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
        
        _offensiveWordLogic.ValidateArticleOffensiveWords(article);
        oldArticle.UpdateAttributes(article);
        _repository.Update(oldArticle);
        _repository.Save();

        return oldArticle;
    }

    public Article ApproveArticle(Guid id, Article article)
    {
        Article? oldArticle = _repository.GetBy(a => a.Id == id);

        ValidateNull(oldArticle);

        article.DateLastModified = DateTime.Now;
        article.DatePublished = oldArticle.DatePublished;
        article.Owner = oldArticle.Owner;
        article.IsPublic = true;
        article.IsApproved = true;
        article.IsEdited = true;
        article.OffensiveContent = _offensiveWordLogic.GetOffensiveWords(article.Content).Concat(_offensiveWordLogic.GetOffensiveWords(article.Title)).ToList();
        
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