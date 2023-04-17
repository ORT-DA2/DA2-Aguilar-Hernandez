using Blog.Domain.Entities;

namespace Blog.BusinessLogic;

public interface ICommentService
{
    Comment AddNewComment(Comment comment);
}