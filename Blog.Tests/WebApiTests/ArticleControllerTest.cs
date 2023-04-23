using System.Net;
using Blog.Domain.Entities;
using Blog.Domain.Enums;
using Blog.IBusinessLogic;
using Blog.Models.Out.User;
using Blog.WebApi.Controllers;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace Blog.Tests.WebApiTests;

[TestClass]
public class ArticleControllerTest
{
    private Mock<IArticleLogic> _articlenMock;
    private Article _articleTest;
    private User _user;

    [TestInitialize]
    public void Setup()
    {
        _articlenMock = new Mock<IArticleLogic>(MockBehavior.Strict);

        _user = new User()
        {
            Id = Guid.NewGuid(),
            FirstName = "Nicolas",
            LastName = "Hernandez",
            Username = "NicolasAHF",
            Email = "nicolas@exmaple.com",
            Password = "123456",
            Roles = new List<UserRole>()

        };
        
        string imageLink =
            "https://upload.wikimedia.org/wikipedia/commons/thumb/c/cf/Angular_full_color_logo.svg/1200px-Angular_full_color_logo.svg.png";
        byte[]? imageData;
        using (WebClient webClient = new WebClient())
        {
            imageData = webClient.DownloadData(imageLink);
        }
        
        _articleTest = new Article()
        {
            Id = Guid.NewGuid(),
            Title = "Angular Webpage",
            Content = "New features about angular are being developed",
            Owner = _user,
            Comments = new List<Comment>(){},
            DateLastModified = DateTime.Now,
            DatePublished = DateTime.Now,
            Image = imageData,
            IsPublic = true,
            Template = Template.RectangleTop
            
        };
    }

    [TestCleanup]
    public void Cleanup()
    {
        _articlenMock.VerifyAll();
    }
    
    [TestMethod]
    public void GetByIdValidArticleTest()
    {
        var controller = new ArticlesController(_articlenMock.Object);
        _articlenMock.Setup(o => o.GetArticleById(_articleTest.Id)).Returns(_articleTest);
        var result = controller.GetArticleById(_articleTest.Id);
        var okResult = result as OkObjectResult;
    }
}