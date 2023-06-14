using Blog.Domain.Entities;
using Blog.IBusinessLogic;
using Blog.IDataAccess;
using Moq;

namespace Blog.BusinessLogic.Test;

[TestClass]
public class NotificationLogicTest
{
    private Article _article;
    private User _articleOwner;
    private User _commentator;
    private Comment _comment;
    private Mock<IRepository<Notification>> _repository;
    private INotificationLogic _notificationLogic;

    [TestInitialize]
    public void SetUp()
    {
        _article = new Article()
        {
            Id = Guid.NewGuid(),
            Title = "title",
            Content = "Content",
            DatePublished = DateTime.Now,
            DateLastModified = DateTime.Now,
            IsPublic = true,
            Comments = new List<Comment>()
        };
        
        _articleOwner = new User()
        {
            Id = Guid.NewGuid(),
            FirstName = "Nicolas",
            LastName = "Hernandez",
            Username = "nico123",
            Email = "nico@gmail.com",
            Password = "password"
        };
        
        _article.Owner = _articleOwner;

        _commentator = new User()
        {
            Id = Guid.NewGuid(),
            FirstName = "Francisco",
            LastName = "Aguilar",
            Username = "Francisco274",
            Email = "fran@gmail.com",
            Password = "password"
        };
        
        _comment = new Comment()
        {
            Id = Guid.NewGuid(),
            Article = _article,
            Body = "Que bien me quedo este post",
            DatePublished = DateTime.Now,
            Owner = _commentator
        };
        _article.Comments.Add(_comment);
        
        _repository = new Mock<IRepository<Notification>>();
        _notificationLogic = new NotificationLogic(_repository.Object);
        
    }
    
    [TestMethod]
    public void SendNotification()
    {
        var notification = new Notification()
        {
            Comment = _comment,
            Id = Guid.NewGuid(),
            IsRead = false,
            UserToNotify = _articleOwner
        };
        _repository.Setup(o => o.Insert(It.IsAny<Notification>()));

        var result = _notificationLogic.SendNotification(notification);
        
        Assert.AreEqual(result.Comment.Id, _comment.Id);
        Assert.AreEqual(result.UserToNotify.Id, _articleOwner.Id);
        Assert.IsFalse(result.IsRead);
        
        _repository.VerifyAll();
    }

    [TestMethod]
    public void TestGetUnreadNotifications()
    {
        Notification notification = new Notification()
        {
            Comment = _comment,
            Id = Guid.NewGuid(),
            IsRead = false,
            UserToNotify = _articleOwner
        };

        List<Notification> expectedNotification = new List<Notification>()
        {
            notification
        };

        _repository.Setup(o => o.GetByUser(It.IsAny<User>())).Returns(expectedNotification);

        var notifications = _notificationLogic.GetUnreadNotificationsByUser(_articleOwner);

        Assert.AreEqual(expectedNotification, notifications);
        Assert.IsTrue(expectedNotification.First().IsRead);
        _repository.VerifyAll();
    }
}
   