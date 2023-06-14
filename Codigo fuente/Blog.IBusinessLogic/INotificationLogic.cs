using Blog.Domain.Entities;

namespace Blog.IBusinessLogic;

public interface INotificationLogic
{
   Notification SendNotification(Notification notification);
   IEnumerable<Notification> GetUnreadNotificationsByUser(User user);
}