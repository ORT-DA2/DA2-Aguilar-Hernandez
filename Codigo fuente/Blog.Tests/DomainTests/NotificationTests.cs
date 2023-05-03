using System.Net;
using Blog.Domain;
using Blog.Domain.Entities;
using Blog.Domain.Enums;
using Microsoft.AspNetCore.Http;
using Moq;

namespace Blog.Tests.DomainTests;

[TestClass]
public class NotificationTests
{
    [TestMethod]
    public void GetAndSetUserTest()
    {
        DateTime time = new DateTime(DateTime.Now.Hour);
        List<Comment> comments = new List<Comment>();
        
        var image = "test.jpg";
        
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
            Image = image,
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
        notification.UserToNotify = user;
        notification.Comment = comment;

        Assert.AreEqual(id, notification.Id);
        Assert.AreEqual(user, notification.UserToNotify);
        Assert.AreEqual(comment, notification.Comment);
    }
}