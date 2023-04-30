using Blog.Domain.Entities;
using Blog.Domain.Exceptions;
using Blog.IBusinessLogic;
using Blog.IDataAccess;

namespace Blog.BusinessLogic;

public class CommentLogic: ICommentLogic
{
    private readonly IRepository<Comment> _repository;
    private readonly ISessionLogic _sessionLogic;
    private readonly IArticleLogic _articleLogic;
    public CommentLogic(IRepository<Comment> repository, ISessionLogic sessionLogic, IArticleLogic articleLogic)
    {
        _repository = repository;
        _sessionLogic = sessionLogic;
        _articleLogic = articleLogic;
    }
    public Comment AddNewComment(Comment comment, Guid authorization, Guid articleId)
    {
        comment.Owner = _sessionLogic.GetLoggedUser(authorization);
        comment.Article = _articleLogic.GetArticleById(articleId); 
        comment.DatePublished = DateTime.Now;
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