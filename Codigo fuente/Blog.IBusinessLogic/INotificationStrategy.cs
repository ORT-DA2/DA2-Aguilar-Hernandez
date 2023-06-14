using Blog.Domain.Entities;

namespace Blog.IBusinessLogic;

public interface INotificationStrategy
{
    public Notification CreateNotification(object post);
    public IEnumerable<Notification> CreateAdminNotification(object post);
}