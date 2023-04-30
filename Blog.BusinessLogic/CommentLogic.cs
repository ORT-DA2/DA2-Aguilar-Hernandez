using Blog.Domain.Entities;
using Blog.Domain.Exceptions;
using Blog.IBusinessLogic;
using Blog.IDataAccess;

namespace Blog.BusinessLogic;

public class CommentLogic: ICommentLogic
{
    private readonly IRepository<Comment> _repository;
    private readonly IRepository<Article> _articleRepository;
    private readonly ISessionLogic _sessionLogic;

    public CommentLogic(IRepository<Comment> repository, IRepository<Article> articleRepository, ISessionLogic sessionLogic)
    {
        _repository = repository;
        _articleRepository = articleRepository;
        _sessionLogic = sessionLogic;
    }


    public Comment AddNewComment(Comment comment, Guid articleId, Guid authorization)
    {
        var article = _articleRepository.GetById(a => a.Id == articleId);
        article.Comments.Add(comment);
        comment.Owner = _sessionLogic.GetLoggedUser(authorization);
        comment.Article = article;
        
        _repository.Insert(comment);
        _repository.Save();
        return comment;
    }

    public void DeleteCommentById(Guid id)
    {
        Comment comment = _repository.GetById(c => c.Id == id);
        
        if (comment == null)
        {
            throw new NotFoundException("The comment was not found");
        }
        
        _repository.Delete(comment);
        _repository.Save();
    }
}