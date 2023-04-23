using Blog.Domain.Entities;

namespace Blog.BusinessLogic;

public interface ICommentService
{
    Comment gitAddNewComment(Comment comment);
    void DeleteCommentById(Guid id);
}