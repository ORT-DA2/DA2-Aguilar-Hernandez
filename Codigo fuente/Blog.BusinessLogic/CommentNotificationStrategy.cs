using Blog.Domain.Entities;
using Blog.Domain.Enums;
using Blog.IBusinessLogic;
using Blog.IDataAccess;

namespace Blog.BusinessLogic;

public class CommentNotificationStrategy: INotificationStrategy
{
    private readonly IRepository<User> _userRepository;
    
    public CommentNotificationStrategy(IRepository<User> userRepository)
    {
        _userRepository = userRepository;
    }
    public Notification CreateNotification(object post)
    {
        Notification notification = new Notification();
        Comment comment = (Comment)post;
        if (comment.IsApproved)
        {
            notification = new Notification()
            {
                Id = Guid.NewGuid(),
                Comment = comment,
                UserToNotify = comment.Owner,
                IsRead = false
            };
        }
        else
        {
            notification = new Notification()
            {
                Id = Guid.NewGuid(),
                Comment = comment,
                UserToNotify = comment.Article.Owner,
                IsRead = false
            };
        }
        
        return notification;
    }
    
    public IEnumerable<Notification> CreateAdminNotification(object post)
    {
        Comment comment = (Comment)post;;
        List<Notification> notifications = new List<Notification>();
        _userRepository.GetAll().Where(u => u.Roles.Any(ur => ur.Role == Role.Admin)).ToList().ForEach(u =>
        {
            Notification notification = new Notification()
            {
                Id = Guid.NewGuid(),
                Comment = comment,
                UserToNotify = u,
                IsRead = false
            };
            notifications.Add(notification);
        });
        return notifications;
    }
}