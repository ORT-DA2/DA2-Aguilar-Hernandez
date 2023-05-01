using Blog.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Blog.DataAccess.Test;

[TestClass]
public class NotificationRepositoryTest
{
    private readonly BlogDbContext _blogContext;
    private readonly NotificationRepository _notificationRepository;
    
    public NotificationRepositoryTest()
    {
        _blogContext = ContextFactory.GetNewContext(ContextType.SQLite);
        _notificationRepository = new NotificationRepository(_blogContext);
    }
    
    [TestInitialize]
    public void SetUp()
    {
        _blogContext.Database.OpenConnection();
        _blogContext.Database.EnsureCreated();
    }
    
    [TestCleanup]
    public void CleanUp()
    {
        _blogContext.Database.EnsureDeleted();
    }

    [TestMethod]
    public void GetUnreadNotificationsByUser()
    {
    
        User nicolas = new User()
        {
            Id = new Guid("b90af3a0-f9d9-436e-b0c5-52b1f78fc567"),
            FirstName = "Nicolas",
            LastName = "Hernandez",
            Username = "NicolasAHF",
            Password = "123456",
            Roles = new List<UserRole> { },
            Email = "nicolas@example.com"
        };
        User francisco = new User()
        {
            Id = Guid.NewGuid(),
            FirstName = "Francisco",
            LastName = "Aguilar",
            Username = "Fran123",
            Password = "123456",
            Roles = new List<UserRole> { },
            Email = "fran@gmail.com"
        };
        Article article = new Article()
        {
            Title = "title",
            Content = "Post",
            DatePublished = DateTime.Now,
            DateLastModified = DateTime.Now,
            Id = Guid.NewGuid(),
            Image = "image.jpg",
            IsPublic = true,
            Owner = nicolas
        };

        var commentFromFrancisco = new Comment()
        {
            Id = new Guid("b90af3a0-f9d9-436e-b0c5-52b1f78fc567"),
            Body = "Buen post",
            Article = article,
            Owner = francisco
        };

        var notification = new Notification()
        {
            Id = Guid.NewGuid(),
            Comment = commentFromFrancisco,
            UserToNotify = nicolas,
            IsRead = false
        };

        _blogContext.Add(notification);
        _blogContext.SaveChanges();

        var elementExpected = notification;
        var elementSaved = _notificationRepository.GetUnreadNotificationsByUser(nicolas.Id);
        Assert.AreEqual(elementExpected, elementSaved.First());
    }
    
}