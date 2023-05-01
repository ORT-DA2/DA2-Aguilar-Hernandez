using Blog.Domain.Entities;
using Blog.IBusinessLogic;
using Blog.IDataAccess;
using Moq;

namespace Blog.BusinessLogic.Test;

[TestClass]
public class NotificationLogicTest
{
    [TestMethod]
    public void SendNotification()
    {
        Article article = new Article()
        {
            Id = Guid.NewGuid(),
            Title = "title",
            Content = "Content",
            DatePublished = DateTime.Now,
            DateLastModified = DateTime.Now,
            IsPublic = true,
            Comments = new List<Comment>()
        };
        
        User postOwner = new User()
        {
            Id = Guid.NewGuid(),
            FirstName = "Nicolas",
            LastName = "Hernandez",
            Username = "nico123",
            Email = "nico@gmail.com",
            Password = "password"
        };
        
        article.Owner = postOwner;

        User commentator = new User()
        {
            Id = Guid.NewGuid(),
            FirstName = "Francisco",
            LastName = "Aguilar",
            Username = "Francisco274",
            Email = "fran@gmail.com",
            Password = "password"
        };
        
        Comment comment = new Comment()
        {
            Id = Guid.NewGuid(),
            Article = article,
            Body = "Que bien me quedo este post",
            DatePublished = DateTime.Now,
            Owner = commentator
        };
        
        article.Comments.Add(comment);
        var repositoryMock = new Mock<IRepository<Notification>>(MockBehavior.Strict);
        var userLogicMock = new Mock<IUserLogic>();
        var notificationLogic = new NotificationLogic(repositoryMock.Object,userLogicMock.Object);


        repositoryMock.Setup(O => O.Insert(It.IsAny<Notification>()));
        repositoryMock.Setup(O => O.Save());
        
        var result = notificationLogic.SendNotification(comment);
        
        Assert.AreEqual(result.Comment.Id, comment.Id);
        Assert.AreEqual(result.UserToNotify.Id, postOwner.Id);
        Assert.AreEqual(result.IsRead, false);
        
        repositoryMock.VerifyAll();
        userLogicMock.VerifyAll();
    }

}