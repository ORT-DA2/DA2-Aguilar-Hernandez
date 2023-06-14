using Blog.Domain.Entities;
using Blog.IBusinessLogic;
using Blog.IDataAccess;

namespace Blog.BusinessLogic;

public class NotificationLogic : INotificationLogic
{
    private readonly IRepository<Notification> _repository;

    public NotificationLogic(IRepository<Notification> repository)
    {
        _repository = repository;
    }
    
    public Notification SendNotification(Notification notification)
    {
        _repository.Insert(notification);
        return notification;
    }

    public IEnumerable<Notification> GetUnreadNotificationsByUser(User user)
    {
        IEnumerable<Notification> notifications = _repository.GetByUser(user);
        foreach (Notification notification in notifications)
        {
            notification.IsRead = true;
        }
        _repository.Save();
        return notifications;
    }

}