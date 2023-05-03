using Blog.Domain.Entities;

namespace Blog.IBusinessLogic;

public interface ICommentLogic
{ 
    Comment AddNewComment(Comment comment, Guid articleId, Guid authorization);
    void DeleteCommentById(Guid id);
    Comment ReplyComment(Guid commentId, string reply);
}