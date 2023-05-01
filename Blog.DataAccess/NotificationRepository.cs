using System.Linq.Expressions;
using Blog.Domain.Entities;

namespace Blog.DataAccess;

public class NotificationRepository : Repository<Notification>
{
    public NotificationRepository(BlogDbContext context) : base(context)
    {
            
    }

    public IEnumerable<Notification> GetUnreadNotificationsByUser(Guid userId)
    {
        return _context.Set<Notification>().Where(n => n.UserToNotify.Id == userId && !n.IsRead);
    }

}