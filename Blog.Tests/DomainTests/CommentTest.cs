using System.Drawing;
using System.Net;
using Blog.Domain;
using Blog.Domain.Entities;
using Blog.Domain.Enums;
using Microsoft.AspNetCore.Http;
using Moq;

namespace Blog.Tests.DomainTests;

[TestClass]
public class CommentTest
{
    [TestMethod]
    public void GetAndSetCommentTest()
    {
        DateTime time = new DateTime(DateTime.Now.Hour);
        List<Comment> comments = new List<Comment>();
        var formFile = new Mock<IFormFile>();
        formFile.Setup(f => f.Length).Returns(1234);
        formFile.Setup(f => f.FileName).Returns("test.jpg");
        formFile.Setup(f => f.ContentType).Returns("image/jpeg");
        
        using var ms = new MemoryStream();
        var image = ms.ToArray();
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
        
        Comment comment = new Comment();
        comment.Id = new Guid();
        Guid id = comment.Id;
        comment.Owner = user;
        comment.Article = article;
        comment.Body = "Nice Article";
        comment.Reply = "Thank you!";

        Assert.AreEqual(id, comment.Id);
        Assert.AreEqual(user, comment.Owner);
        Assert.AreEqual(article, comment.Article);
        Assert.AreEqual("Nice Article", comment.Body);
        Assert.AreEqual("Thank you!", comment.Reply);
    }
}