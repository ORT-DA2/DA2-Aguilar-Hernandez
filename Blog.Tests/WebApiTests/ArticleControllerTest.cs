using System.Net;
using Blog.Domain.Entities;
using Blog.Domain.Enums;
using Blog.Filters.Exceptions;
using Blog.IBusinessLogic;
using Blog.Models.In.Article;
using Blog.Models.Out.Article;
using Blog.Models.Out.User;
using Blog.WebApi.Controllers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace Blog.Tests.WebApiTests;

[TestClass]
public class ArticleControllerTest
{
    private Mock<IArticleLogic> _articlenMock;
    private CreateArticleDTO _articleTestDTO;
    private User _user;
    private List<Article> _articles;
    private Article _articleTest;
    private Article _articleTest2;
    private Article _articleTest3;
    private Article _articleTest4;
    private Article _articleTest5;
    private Article _articleTest6;
    private Article _articleTest7;
    private Article _articleTest8;
    private Article _articleTest9;
    private Article _articleTest10;
    private Article _articleTest11;

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

        var formFile = new Mock<IFormFile>();
        formFile.Setup(f => f.Length).Returns(1234);
        formFile.Setup(f => f.FileName).Returns("test.jpg");
        formFile.Setup(f => f.ContentType).Returns("image/jpeg");
        
        using var ms = new MemoryStream();
        var image = ms.ToArray();
        
        _articleTestDTO = new CreateArticleDTO()
        {
            Title = "Angular Webpage",
            Content = "New features about angular are being developed",
            Owner = _user,
            Comments = new List<Comment>(){},
            DateLastModified = DateTime.Now,
            DatePublished = DateTime.Now,
            Image = formFile.ToString(),
            IsPublic = true,
            Template = Template.RectangleTop
        };

        _articleTest = _articleTestDTO.ToEntity(_articleTestDTO.Image);
        
        _articleTest2 = new Article()
        {
            Id = Guid.NewGuid(),
            Title = ".NET 6 Webpage",
            Content = "New features about .NET are being developed",
            Owner = _user,
            Comments = new List<Comment>(){},
            DateLastModified = DateTime.Now,
            DatePublished = DateTime.Now,
            Image = image,
            IsPublic = true,
            Template = Template.RectangleTop
            
        };
        
        _articleTest3 = new Article()
        {
            Id = Guid.NewGuid(),
            Title = "Test3",
            Content = "Uruguay is a country in south america",
            Owner = _user,
            Comments = new List<Comment>(){},
            DateLastModified = DateTime.Now,
            DatePublished = DateTime.Now,
            Image = image,
            IsPublic = true,
            Template = Template.RectangleTop
            
        };
        
        _articleTest4 = new Article()
        {
            Id = Guid.NewGuid(),
            Title = "Test4",
            Content = "Uruguay is a country in south america",
            Owner = _user,
            Comments = new List<Comment>(){},
            DateLastModified = DateTime.Now,
            DatePublished = DateTime.Now,
            Image = image,
            IsPublic = true,
            Template = Template.RectangleTop
            
        };
        
        _articleTest5 = new Article()
        {
            Id = Guid.NewGuid(),
            Title = "Test5",
            Content = "Uruguay is a country in south america",
            Owner = _user,
            Comments = new List<Comment>(){},
            DateLastModified = DateTime.Now,
            DatePublished = DateTime.Now,
            Image = image,
            IsPublic = true,
            Template = Template.RectangleTop
            
        };
        
        _articleTest6 = new Article()
        {
            Id = Guid.NewGuid(),
            Title = "Test6",
            Content = "Uruguay is a country in south america",
            Owner = _user,
            Comments = new List<Comment>(){},
            DateLastModified = DateTime.Now,
            DatePublished = DateTime.Now,
            Image = image,
            IsPublic = true,
            Template = Template.RectangleTop
            
        };
        
        _articleTest7 = new Article()
        {
            Id = Guid.NewGuid(),
            Title = "Test7",
            Content = "Uruguay is a country in south america",
            Owner = _user,
            Comments = new List<Comment>(){},
            DateLastModified = DateTime.Now,
            DatePublished = DateTime.Now,
            Image = image,
            IsPublic = true,
            Template = Template.RectangleTop
            
        };
        
        _articleTest8 = new Article()
        {
            Id = Guid.NewGuid(),
            Title = "Test8",
            Content = "Uruguay is a country in south america",
            Owner = _user,
            Comments = new List<Comment>(){},
            DateLastModified = DateTime.Now,
            DatePublished = DateTime.Now,
            Image = image,
            IsPublic = true,
            Template = Template.RectangleTop
            
        };
        
        _articleTest9 = new Article()
        {
            Id = Guid.NewGuid(),
            Title = "Test9",
            Content = "Uruguay is a country in south america",
            Owner = _user,
            Comments = new List<Comment>(){},
            DateLastModified = DateTime.Now,
            DatePublished = DateTime.Now,
            Image = image,
            IsPublic = true,
            Template = Template.RectangleTop
            
        };
        
        _articleTest10 = new Article()
        {
            Id = Guid.NewGuid(),
            Title = "Test10",
            Content = "Uruguay is a country in south america",
            Owner = _user,
            Comments = new List<Comment>(){},
            DateLastModified = DateTime.Now,
            DatePublished = DateTime.Now,
            Image = image,
            IsPublic = true,
            Template = Template.RectangleTop
            
        };
        
        _articleTest11 = new Article()
        {
            Id = Guid.NewGuid(),
            Title = "Test11",
            Content = "Uruguay is a country in south america",
            Owner = _user,
            Comments = new List<Comment>(){},
            DateLastModified = DateTime.Now,
            DatePublished = DateTime.Now,
            Image = image,
            IsPublic = true,
            Template = Template.RectangleTop
            
        };

        _articles = new List<Article>()
        {
            _articleTest,
            _articleTest2,
            _articleTest3,
            _articleTest4,
            _articleTest5,
            _articleTest6,
            _articleTest7,
            _articleTest8,
            _articleTest9,
            _articleTest10,
            _articleTest11
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
        var value = okResult.Value as ArticleDetailDTO;
        
        Assert.IsTrue(_articleTest.Id.Equals(value.Id));
        Assert.IsTrue(_articleTest.Title.Equals(value.Title));
        Assert.IsTrue(_articleTest.Content.Equals(value.Content));
        Assert.IsTrue(_articleTest.Owner.Equals(value.Owner));
        Assert.IsTrue(_articleTest.Comments.Equals(value.Comments));
        Assert.IsTrue(_articleTest.DatePublished.Equals(value.DatePublished));
        Assert.IsTrue(_articleTest.DateLastModified.Equals(value.DateLastModified));
        Assert.IsTrue(_articleTest.Image.Equals(value.Image));
        Assert.IsTrue(_articleTest.IsPublic.Equals(value.IsPublic));
        Assert.IsTrue(_articleTest.Template.Equals(value.Template));
    }

    [TestMethod]
    public void GetByIdInvalidArticleTest()
    {
        var controller = new ArticlesController(_articlenMock.Object);
        _articlenMock.Setup(o => o.GetArticleById(_articleTest.Id)).Throws(new NotFoundException("There are no articles with the id"));
        var result = controller.GetArticleById(_articleTest.Id);

        Assert.IsInstanceOfType(result, typeof(NotFoundObjectResult));
    }

    [TestMethod]
    public void GetArticleByTextValidTest()
    {
        string textRecibed = "New";

        List<Article> _articlesFilteres = new List<Article>()
        {
            _articleTest,
            _articleTest2
        };
        
        var controller = new ArticlesController(_articlenMock.Object);
        _articlenMock.Setup(o => o.GetArticleByText(textRecibed)).Returns(_articlesFilteres);
        var result = controller.GetArticleByText(textRecibed);
        var okResult = result as OkObjectResult;
        var value = okResult.Value as List<Article>;
        
        Assert.AreEqual(_articlesFilteres, value);
    }
    
    [TestMethod]
    public void GetArticleByTextFailTest()
    {
        string textRecibed = "Hello";
        
        
        var controller = new ArticlesController(_articlenMock.Object);
        _articlenMock.Setup(o => o.GetArticleByText(textRecibed)).Throws(new NotFoundException("There are no articles with that text."));;
        var result = controller.GetArticleByText(textRecibed);
        
        Assert.IsInstanceOfType(result, typeof(NotFoundObjectResult));
    }

    [TestMethod]
    public void GetAllArticlesValidTest()
    {
        List<Article> articles = new List<Article>()
        {
            _articleTest,
            _articleTest2
        };
        
        var controller = new ArticlesController(_articlenMock.Object);
        _articlenMock.Setup(o => o.GetAllArticles()).Returns(articles);
        var result = controller.GetAllArticles();
        var okResult = result as OkObjectResult;
        var dto = okResult.Value as List<Article>;
        Assert.AreEqual(articles, dto);
    }
    
    [TestMethod]
    public void GetAllArticlesInvalidTest()
    {
        var controller = new ArticlesController(_articlenMock.Object);
        _articlenMock.Setup(o => o.GetAllArticles()).Throws(new NotFoundException("There are no articles."));
        var result = controller.GetAllArticles();
        Assert.IsInstanceOfType(result, typeof(NotFoundObjectResult));
    }
    
    [TestMethod]
    public void CreateValidArticleTest()
    {
        var controller = new ArticlesController(_articlenMock.Object);
        _articlenMock.Setup(o => o.CreateArticle(It.IsAny<Article>())).Returns(_articleTest);
        var result = controller.CreateArticle(_articleTestDTO);
        var okResult = result as CreatedResult;
        var dto = okResult.Value as ArticleDetailDTO;
        Assert.AreEqual(_articleTest.Title, dto.Title);
    }
    
    [TestMethod]
    public void CreateInvalidArticleTest()
    {
        var controller = new ArticlesController(_articlenMock.Object);
        _articlenMock.Setup(o => o.CreateArticle(It.IsAny<Article>())).Throws(new ArgumentException());
        var result = controller.CreateArticle(_articleTestDTO);
        Assert.IsInstanceOfType(result, typeof(BadRequestObjectResult));
    }
    
    [TestMethod]
    public void UpdateValidArticleTest()
    {
        var token = Guid.NewGuid();
        
        var controller = new ArticlesController(_articlenMock.Object);
        _articlenMock.Setup(o => o.UpdateArticle(_articleTest.Id, It.IsAny<Article>(), token)).Returns(_articleTest);
        var result = controller.UpdateArticle(_articleTest.Id ,_articleTestDTO, token);
        var okResult = result as CreatedResult;
        var dto = okResult.Value as ArticleDetailDTO;
        Assert.AreEqual(_articleTest.Title, dto.Title);
    }
    
    [TestMethod]
    public void UpdateArticleFailTest()
    {
        var token = Guid.NewGuid();
        
        var controller = new ArticlesController(_articlenMock.Object);
        _articlenMock.Setup(o => o.UpdateArticle(_articleTest.Id, It.IsAny<Article>(), token)).Throws(new ArgumentException());
        var result = controller.UpdateArticle(_articleTest.Id ,_articleTestDTO, token);
        Assert.IsInstanceOfType(result, typeof(BadRequestObjectResult));
    }

    [TestMethod]
    public void DeleteArticleSuccessTest()
    {
        var controller = new ArticlesController(_articlenMock.Object);
        _articlenMock.Setup(o => o.DeleteArticle(_articleTest.Id));
        var result = controller.DeleteArticle(_articleTest.Id);
        var okResult = result as OkObjectResult;
        Assert.AreEqual(okResult.Value, $"Article with the id {_articleTest.Id} was deleted");
    }
    
    [TestMethod]
    public void DeleteArticleFailTest()
    {
        var controller = new ArticlesController(_articlenMock.Object);
        _articlenMock.Setup(o => o.DeleteArticle(_articleTest.Id)).Throws(new NotFoundException("There are no articles."));
        var result = controller.DeleteArticle(_articleTest.Id);
        Assert.IsInstanceOfType(result, typeof(NotFoundObjectResult));
    }

    [TestMethod]
    public void GetLastTenSuccessfulTest()
    {
        var articles = _articles;
        articles.Remove(_articleTest11);
        var controller = new ArticlesController(_articlenMock.Object);
        _articlenMock.Setup(o => o.GetLastTen()).Returns(articles);
        var result = controller.GetLastTen();
        var okResult = result as OkObjectResult;
        var dto = okResult.Value as List<Article>;
        Assert.AreEqual(articles, dto);
    }
    
    [TestMethod]
    public void GetLastTenInvalidTest()
    {
        var controller = new ArticlesController(_articlenMock.Object);
        _articlenMock.Setup(o => o.GetLastTen()).Throws(new NotFoundException("There are no articles."));
        var result = controller.GetLastTen();
        Assert.IsInstanceOfType(result, typeof(NotFoundObjectResult));
    }
}