using Blog.Domain.Entities;

namespace Blog.IBusinessLogic;

public interface INotificationLogic
{
   Notification SendNotification(Comment comment);
   IEnumerable<Notification> GetNotificationsByUser(Guid userId);
}