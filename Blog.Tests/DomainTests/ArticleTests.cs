using System.Net;
using Blog.Domain.Entities;

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
        string imageLink =
            "https://upload.wikimedia.org/wikipedia/commons/thumb/c/cf/Angular_full_color_logo.svg/1200px-Angular_full_color_logo.svg.png";
        byte[]? imageData;
        using (WebClient webClient = new WebClient())
        {
            imageData = webClient.DownloadData(imageLink);
        }
        
        Article article = new Article();
        article.Id = new Guid();
        Guid id = article.Id;
        article.Title = "Learn Angular";
        article.Content = "Angular is a frontend framework";
        article.Owner = user;
        article.IsPublic = true;
        article.Image = imageData;
        article.DatePublished = time;
        article.DateLastModified = time;
        article.Comments = comments;
                
                
        Assert.AreEqual(id, article.Id);
        Assert.AreEqual("Learn Angular", article.Title);
        Assert.AreEqual("Angular is a frontend framework", article.Content);
        Assert.AreEqual(user, article.Owner);
        Assert.AreEqual(true, article.IsPublic);
        Assert.AreEqual(imageData, article.Image);
        Assert.AreEqual(time, article.DatePublished);
        Assert.AreEqual(time, article.DateLastModified);
        Assert.AreEqual(comments, article.Comments);
    }
    
}