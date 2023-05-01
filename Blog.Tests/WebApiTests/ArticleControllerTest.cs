using System.Net;
using Blog.Domain.Entities;
using Blog.Domain.Enums;
using Blog.Domain.Exceptions;
using Blog.IBusinessLogic;
using Blog.Models.In;
using Blog.Models.Out;
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

        var image = "test.jpg";
        
        _articleTestDTO = new CreateArticleDTO()
        {
            Title = "Angular Webpage",
            Content = "New features about angular are being developed",
            Image = image,
            IsPublic = true,
            Template = Template.RectangleTop
        };

        _articleTest = _articleTestDTO.ToEntity();
        
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
            IsPublic = false,
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
            IsPublic = false,
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
        _articlenMock.Setup(o => o.GetArticleById(_articleTest2.Id)).Returns(_articleTest2);
        var result = controller.GetArticleById(_articleTest2.Id);
        var okResult = result as OkObjectResult;
        var value = okResult.Value as ArticleDetailDTO;
        
        Assert.IsTrue(_articleTest2.Id.Equals(value.Id));
        Assert.IsTrue(_articleTest2.Title.Equals(value.Title));
        Assert.IsTrue(_articleTest2.Content.Equals(value.Content));
        Assert.IsTrue(_articleTest2.Comments.Count.Equals(value.Comments.Count));
        Assert.IsTrue(_articleTest2.DatePublished.Equals(value.DatePublished));
        Assert.IsTrue(_articleTest2.DateLastModified.Equals(value.DateLastModified));
        Assert.IsTrue(_articleTest2.Image.Equals(value.Image));
        Assert.IsTrue(_articleTest2.IsPublic.Equals(value.IsPublic));
        Assert.IsTrue(_articleTest2.Template.Equals(value.Template));
    }

    [TestMethod]
    [ExpectedException(typeof(NotFoundException),
        "There are no articles with the id")]
    public void GetByIdInvalidArticleTest()
    {
        var controller = new ArticlesController(_articlenMock.Object);
        _articlenMock.Setup(o => o.GetArticleById(_articleTest.Id)).Throws(new NotFoundException("There are no articles with the id"));
        controller.GetArticleById(_articleTest.Id);
    }

    [TestMethod]
    public void GetArticleByTextValidTest()
    {
        string textRecibed = "New";

        List<Article> _articlesFilteres = new List<Article>()
        {
            _articleTest2,
            _articleTest3
        };
        
        var controller = new ArticlesController(_articlenMock.Object);
        _articlenMock.Setup(o => o.GetArticleByText(textRecibed)).Returns(_articlesFilteres);
        var result = controller.GetArticleByText(textRecibed);
        var okResult = result as OkObjectResult;
        var value = okResult.Value as List<ArticleDetailDTO>;
        
        Assert.AreEqual(_articlesFilteres.Count, value.Count);
    }
    
    [TestMethod]
    [ExpectedException(typeof(NotFoundException),
        "There are no articles with that text.")]
    public void GetArticleByTextFailTest()
    {
        string textRecibed = "Hello";
        var controller = new ArticlesController(_articlenMock.Object);
        _articlenMock.Setup(o => o.GetArticleByText(textRecibed)).Throws(new NotFoundException("There are no articles with that text."));;
        controller.GetArticleByText(textRecibed);
    }

    [TestMethod]
    public void GetAllArticlesValidTest()
    {
        List<Article> articles = new List<Article>()
        {
            _articleTest2,
            _articleTest3
        };
        var articlesDTO = new List<ArticleDetailDTO>();
        foreach (var article in articles)
        {
            articlesDTO.Add(new ArticleDetailDTO(article));
        }
        
        var controller = new ArticlesController(_articlenMock.Object);
        _articlenMock.Setup(o => o.GetAllArticles()).Returns(articles);
        var result = controller.GetAllArticles();
        var okResult = result as OkObjectResult;
        var dto = okResult.Value as List<ArticleDetailDTO>;
        Assert.AreEqual(articlesDTO.Count, dto.Count());
    }
    
    [TestMethod]
    [ExpectedException(typeof(NotFoundException),
        "There are no articles.")]
    public void GetAllArticlesInvalidTest()
    {
        var controller = new ArticlesController(_articlenMock.Object);
        _articlenMock.Setup(o => o.GetAllArticles()).Throws(new NotFoundException("There are no articles."));
        controller.GetAllArticles();
    }
    
    [TestMethod]
    public void CreateValidArticleTest()
    {
        var token = Guid.NewGuid();
        
        var controller = new ArticlesController(_articlenMock.Object);
        _articlenMock.Setup(o => o.CreateArticle(It.IsAny<Article>(), token)).Returns(_articleTest2);
        var result = controller.CreateArticle(_articleTestDTO, token);
        var okResult = result as CreatedResult;
        var dto = okResult.Value as ArticleDetailDTO;
        Assert.AreEqual(_articleTest2.Title, dto.Title);
    }
    
    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public void CreateInvalidArticleTest()
    {
        var token = Guid.NewGuid();
        
        var controller = new ArticlesController(_articlenMock.Object);
        _articlenMock.Setup(o => o.CreateArticle(It.IsAny<Article>(), token)).Throws(new ArgumentException());
        var result = controller.CreateArticle(_articleTestDTO, token);
        Assert.IsInstanceOfType(result, typeof(BadRequestObjectResult));
    }
    
    [TestMethod]
    public void UpdateValidArticleTest()
    {
        var token = Guid.NewGuid();
        var article = _articleTest2;
        var articlenMock = new Mock<IArticleLogic>(MockBehavior.Loose);
        
        var controller = new ArticlesController(articlenMock.Object);
        articlenMock.Setup(o => o.UpdateArticle(_articleTest2.Id, It.IsAny<Article>(), token)).Returns(article);
        var result = controller.UpdateArticle(article.Id ,_articleTestDTO, token);
        var okResult = result as CreatedResult;
        var dto = okResult.Value as ArticleDetailDTO;
        Assert.AreEqual(_articleTest2.Title, dto.Title);
    }
    
    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
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
        var token = Guid.NewGuid();
        
        var controller = new ArticlesController(_articlenMock.Object);
        _articlenMock.Setup(o => o.DeleteArticle(_articleTest.Id, token));
        var result = controller.DeleteArticle(_articleTest.Id, token);
        var okResult = result as OkObjectResult;
        Assert.AreEqual(okResult.Value, $"Article with the id {_articleTest.Id} was deleted");
    }
    
    [TestMethod]
    [ExpectedException(typeof(NotFoundException))]
    public void DeleteArticleFailTest()
    {
        var token = Guid.NewGuid();
        
        var controller = new ArticlesController(_articlenMock.Object);
        _articlenMock.Setup(o => o.DeleteArticle(_articleTest.Id, token)).Throws(new NotFoundException("There are no articles."));
        var result = controller.DeleteArticle(_articleTest.Id, token);
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
    [ExpectedException(typeof(NotFoundException))]
    public void GetLastTenInvalidTest()
    {
        var controller = new ArticlesController(_articlenMock.Object);
        _articlenMock.Setup(o => o.GetLastTen()).Throws(new NotFoundException("There are no articles."));
        var result = controller.GetLastTen();
        Assert.IsInstanceOfType(result, typeof(NotFoundObjectResult));
    }
    
    [TestMethod]
    public void GetPublicArticleSuccessTest()
    {
        var articles = _articles;
        articles.Remove(_articleTest5);
        articles.Remove(_articleTest7);
        
        var controller = new ArticlesController(_articlenMock.Object);
        _articlenMock.Setup(o => o.GetAllPublicArticles()).Returns(articles);
        var result = controller.GetAllPublicArticles();
        var okResult = result as OkObjectResult;
        var dto = okResult.Value as List<ArticleDetailDTO>;
        Assert.AreEqual(articles.Count, dto.Count);
    }
    
    [TestMethod]
    public void GetUsersArticleSuccessTest()
    {
        var articles = new List<Article>()
        {
            _articleTest5,
            _articleTest7
        };
        
        var token = Guid.NewGuid();

        var controller = new ArticlesController(_articlenMock.Object);
        _articlenMock.Setup(o => o.GetAllUserArticles(_user.Username, token)).Returns(articles);
        var result = controller.GetAllUserArticles(_user.Username, token);
        var okResult = result as OkObjectResult;
        var dto = okResult.Value as List<ArticleDetailDTO>;
        Assert.AreEqual(articles.Count, dto.Count);
    }
}