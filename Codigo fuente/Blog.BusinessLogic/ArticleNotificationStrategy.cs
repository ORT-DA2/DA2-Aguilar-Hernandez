using Blog.Domain.Entities;
using Blog.Domain.Enums;
using Blog.IBusinessLogic;
using Blog.IDataAccess;

namespace Blog.BusinessLogic;

public class ArticleNotificationStrategy: INotificationStrategy
{
    private readonly IRepository<User> _userRepository;
    
    public ArticleNotificationStrategy(IRepository<User> userRepository)
    {
        _userRepository = userRepository;
    }
    public Notification CreateNotification(object post)
    {
        Article article = (Article)post;
        Notification notification = new Notification()
        {
            Id = Guid.NewGuid(),
            Article = article,
            UserToNotify = article.Owner,
            IsRead = false
        };
        return notification;
    }
    
    public IEnumerable<Notification> CreateAdminNotification(object post)
    {
        Article article = (Article)post;
        List<Notification> notifications = new List<Notification>();
        _userRepository.GetAll().Where(u => u.Roles.Any(ur => ur.Role == Role.Admin)).ToList().ForEach(u =>
        {
            Notification notification = new Notification()
            {
                Id = Guid.NewGuid(),
                Article = article,
                UserToNotify = u,
                IsRead = false
            };
            notifications.Add(notification);
        });
        return notifications;
    }
}