using Blog.Domain.Entities;
using Blog.Domain.Enums;
using Blog.Filters.Exceptions;
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
        return _repository.GetLastTen();
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
        var oldArticle = _repository.GetById(a => a.Id == id);

        if (oldArticle == null)
        {
            throw new NotFoundException("The article was not found");
        }

        /*if (_sessionLogic.GetLoggedUser(auth).Roles.All(ur => ur.Role != Role.Admin ))
        {
            if (_sessionLogic.GetLoggedUser(auth).Id != id)
            {
                throw new ArgumentException("You can´t update other user");
            }
        }*/

        oldArticle.UpdateAttributes(article);
        _repository.Update(oldArticle);
        _repository.Save();

        return oldArticle;
    }

    public void DeleteArticle(Guid articleId)
    {
        throw new NotImplementedException();
    }
    
}