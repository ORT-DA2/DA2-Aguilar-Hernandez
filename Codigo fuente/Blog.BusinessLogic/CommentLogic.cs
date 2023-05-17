using Blog.Domain.Entities;
using Blog.Domain.Exceptions;
using Blog.IBusinessLogic;
using Blog.IDataAccess;

namespace Blog.BusinessLogic;

public class CommentLogic: ICommentLogic
{
    private readonly IRepository<Comment> _repository;
    private readonly IArticleLogic _articleLogic;
    private readonly ISessionLogic _sessionLogic;
    private static IOffensiveWordLogic _offensiveWordLogic;

    public CommentLogic(IRepository<Comment> repository, IArticleLogic articleLogic, ISessionLogic sessionLogic, IOffensiveWordLogic offensiveWordLogic)
    {
        _repository = repository;
        _articleLogic = articleLogic;
        _sessionLogic = sessionLogic;
        _offensiveWordLogic = offensiveWordLogic;
    }


    public Comment AddNewComment(Comment comment, Guid articleId, Guid authorization)
    {
        var article = _articleLogic.GetArticleById(articleId);
        article.Comments.Add(comment);
        comment.Owner = _sessionLogic.GetLoggedUser(authorization);
        comment.Article = article;
        comment.DatePublished = DateTime.Now;
        _offensiveWordLogic.ValidateCommentOffensiveWords(comment);
        _repository.Insert(comment);
        _repository.Save();
        return comment;
    }

    public void DeleteCommentById(Guid id)
    {
        Comment comment = _repository.GetBy(c => c.Id == id);
        
        ValidateNull(comment);
        
        _repository.Delete(comment);
        _repository.Save();
    }

    public Comment ReplyComment(Guid commentId, string reply)
    {
        Comment comment = _repository.GetBy(c => c.Id == commentId);

        ValidateNull(comment);

        comment.Reply = reply;
        
        _repository.Update(comment);
        _repository.Save();
        return comment;
    }
    
    public void ValidateNull(Comment comment)
    {
        if (comment == null)
        {
            throw new NotFoundException("The comment was not found");
        }
    }
}