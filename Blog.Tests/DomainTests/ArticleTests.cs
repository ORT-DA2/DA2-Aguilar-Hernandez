using System.Net;
using Blog.Domain.Entities;
using Microsoft.AspNetCore.Http;
using Moq;

namespace Blog.Tests.DomainTests;

[TestClass]
public class ArticleTests
{
    [TestMethod]
    public void GetAndSetArticleTest()
    {
        DateTime time = new DateTime(DateTime.Now.Hour);
        User user = new User(){
            FirstName = "Nicolas",
            LastName = "Hernandez",
            Username = "NicolasAHF",
            Email = "nicolashernandez@example.com",
            Roles = new List<UserRole>{}
        };
        List<Comment> comments = new List<Comment>();
        var formFile = new Mock<IFormFile>();
        formFile.Setup(f => f.Length).Returns(1234);
        formFile.Setup(f => f.FileName).Returns("test.jpg");
        formFile.Setup(f => f.ContentType).Returns("image/jpeg");
        
        using var ms = new MemoryStream();
        var image = ms.ToArray();
        
        Article article = new Article();
        article.Id = new Guid();
        Guid id = article.Id;
        article.Title = "Learn Angular";
        article.Content = "Angular is a frontend framework";
        article.Owner = user;
        article.IsPublic = true;
        article.Image = image;
        article.DatePublished = time;
        article.DateLastModified = time;
        article.Comments = comments;
                
                
        Assert.AreEqual(id, article.Id);
        Assert.AreEqual("Learn Angular", article.Title);
        Assert.AreEqual("Angular is a frontend framework", article.Content);
        Assert.AreEqual(user, article.Owner);
        Assert.AreEqual(true, article.IsPublic);
        Assert.AreEqual(image, article.Image);
        Assert.AreEqual(time, article.DatePublished);
        Assert.AreEqual(time, article.DateLastModified);
        Assert.AreEqual(comments, article.Comments);
    }
    
}