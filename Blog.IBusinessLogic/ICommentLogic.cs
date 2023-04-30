using Blog.Domain.Entities;

namespace Blog.IBusinessLogic;

public interface ICommentLogic
{
    Comment AddNewComment(Comment comment, Guid authorization, Guid articleId);
    void DeleteCommentById(Guid id);
}