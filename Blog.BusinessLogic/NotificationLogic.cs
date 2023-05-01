using Blog.Domain.Entities;
using Blog.IBusinessLogic;
using Blog.IDataAccess;

namespace Blog.BusinessLogic;

public class NotificationLogic : INotificationLogic
{
    private readonly IRepository<Notification> _repository;
    private readonly IUserLogic _userLogic;

    public NotificationLogic(IRepository<Notification> repository, IUserLogic userLogic)
    {
        _repository = repository;
        _userLogic = userLogic;
    }
    
    public Notification SendNotification(Comment comment)
    {
        Notification notification = new Notification()
        {
            Id = Guid.NewGuid(),
            Comment = comment,
            UserToNotify = comment.Article.Owner,
            IsRead = false
        };
        // Send notification even if made a comment in a self post
        _repository.Insert(notification);
        _repository.Save();
        return notification;
    }

    public IEnumerable<Notification> GetNotificationsByUser(User user)
    {
        // TODO
        throw new NotImplementedException();
    }

}