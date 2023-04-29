using Blog.Domain.Entities;

namespace Blog.IBusinessLogic;

public interface ICommentLogic
{
    Comment AddNewComment(Comment comment);
    void DeleteCommentById(Guid id);
}