using Blog.Domain.Entities;
using Blog.IDataAccess;

namespace Blog.BusinessLogic;

public class CommentService : ICommentService
{
    private readonly IRepository<Comment> _repository;

    public CommentService(IRepository<Comment> repository)
    {
        _repository = repository;
    }


    public Comment AddNewComment(Comment comment)
    {
        _repository.Insert(comment);
        _repository.Save();
        return comment;
    }

    public void DeleteCommentById(Guid id)
    {
        throw new NotImplementedException();
    }
}