using Blog.Domain.Entities;

namespace Blog.IBusinessLogic;

public interface INotificationLogic
{
   Notification SendNotification(Comment comment);
   IEnumerable<Notification> GetUnreadNotificationsByUser(User user);
}