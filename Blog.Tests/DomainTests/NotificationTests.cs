using Blog.Domain;
using Blog.Domain.Entities;
using Blog.Domain.Enums;

namespace Blog.Tests.DomainTests;

[TestClass]
public class NotificationTests
{
    [TestMethod]
    public void GetAndSetUserTest()
    {
        DateTime time = new DateTime(DateTime.Now.Hour);
        List<Comment> comments = new List<Comment>();
        string linkImage = "https://upload.wikimedia.org/wikipedia/commons/thumb/c/cf/Angular_full_color_logo.svg/1200px-Angular_full_color_logo.svg.png";
        User user = new User(){
            FirstName = "Nicolas",
            LastName = "Hernandez",
            Username = "NicolasAHF",
            Email = "nicolashernandez@example.com",
            Roles = new List<UserRole>{}
        };
        Article article = new Article()
        {
            Owner = user,
            Title = "Learn Angular",
            Content = "Angular is a frontend framework",
            IsPublic = true,
            Image = linkImage,
            DatePublished = time,
            DateLastModified = time,
            Comments = comments
        };
        Comment comment = new Comment()
        {
            Article = article,
            Owner = user,
            Body = "Nice",
            Reply = ""
        };
        comments.Add(comment);
        
        Notification notification = new Notification();
        notification.Id = new Guid();
        Guid id = notification.Id;
        notification.Owner = user;
        notification.Article = article;
        notification.Comment = comment;

        Assert.AreEqual(id, notification.Id);
        Assert.AreEqual(user, notification.Owner);
        Assert.AreEqual(article, notification.Article);
        Assert.AreEqual(comment, notification.Comment);
    }
}