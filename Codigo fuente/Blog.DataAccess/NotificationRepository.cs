using System.Linq.Expressions;
using Blog.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Blog.DataAccess;

public class NotificationRepository : Repository<Notification>
{
    public NotificationRepository(BlogDbContext context) : base(context)
    {
            
    }

    public override IEnumerable<Notification> GetByUser(User user)
    {
        return _context.Set<Notification>()
            .Include(n=>n.UserToNotify)
            .Include(n=>n.Comment)
            .Where(n => n.UserToNotify.Id == user.Id && !n.IsRead).ToList();
    }

}