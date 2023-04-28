using System.Linq.Expressions;
using Blog.BusinessLogic;
using Blog.DataAccess;
using Blog.Domain.Entities;
using Blog.Domain.Enums;
using Blog.IBusinessLogic;
using Blog.IDataAccess;
using Microsoft.AspNetCore.Http;
using Moq;

namespace Blog.Tests.BusinessLogicTests;

[TestClass]
public class ArticleLogicTests
{
    private Mock<IRepository<Article>> _articleRepoMock;
    private Mock<ISessionLogic> _sessionLogicMock;
    private Article _articleTest;
    private List<Article> _articles;
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
        _articleRepoMock = new Mock<IRepository<Article>>(MockBehavior.Strict);
        _sessionLogicMock = new Mock<ISessionLogic>(MockBehavior.Strict);
        
        var user = new User()
        {
            Id = Guid.NewGuid(),
            FirstName = "Nicolas",
            LastName = "Hernandez",
            Username = "NicolasAHF",
            Password = "123456",
            Roles = new List<UserRole>{},
            Email = "nicolas@example.com"
        };

        var role = new UserRole()
        {
            Role = Role.Blogger,
            UserId = user.Id,
            User = user
        };
        
        user.Roles.Add(role);
        
        var formFile = new Mock<IFormFile>();
        formFile.Setup(f => f.Length).Returns(1234);
        formFile.Setup(f => f.FileName).Returns("test.jpg");
        formFile.Setup(f => f.ContentType).Returns("image/jpeg");
        
        using var ms = new MemoryStream();
        var image = ms.ToArray();
        
        _articleTest = new Article()
        {
            Id = Guid.NewGuid(),
            Title = ".NET 6 Webpage",
            Content = "New features about .NET are being developed",
            Owner = user,
            Comments = new List<Comment>(){},
            DateLastModified = DateTime.Now,
            DatePublished = DateTime.Now,
            Image = image,
            IsPublic = true,
            Template = Template.RectangleTop
            
        };
        
        _articleTest2 = new Article()
        {
            Id = Guid.NewGuid(),
            Title = ".NET 6 Webpage",
            Content = "New features about .NET are being developed",
            Owner = user,
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
            Owner = user,
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
            Owner = user,
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
            Owner = user,
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
            Owner = user,
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
            Owner = user,
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
            Owner = user,
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
            Owner = user,
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
            Owner = user,
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
            Owner = user,
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
        _articleRepoMock.VerifyAll();
        _sessionLogicMock.VerifyAll();
    }

    [TestMethod]
    public void CreateArticleSuccessTest()
    {
        var logic = new ArticleLogic(_articleRepoMock.Object, _sessionLogicMock.Object);
        _articleRepoMock.Setup(o => o.Insert(It.IsAny<Article>()));
        _articleRepoMock.Setup(o => o.Save());
        var result = logic.CreateArticle(_articleTest);
        Assert.AreEqual(_articleTest, result);
    }
    
    [TestMethod]
    public void GetAllArticlesValidTest()
    {
        var logic = new ArticleLogic(_articleRepoMock.Object, _sessionLogicMock.Object);
        _articleRepoMock.Setup(o => o.GetAll()).Returns(_articles);
        var result = logic.GetAllArticles();
        Assert.AreEqual(_articles.Count(), result.Count());
    }
    
    [TestMethod]
    public void GetArticleByIdValidTest()
    {
        var logic = new ArticleLogic(_articleRepoMock.Object, _sessionLogicMock.Object);
        _articleRepoMock.Setup(o => o.GetById(It.IsAny<Expression<Func<Article, bool>>>())).Returns(_articleTest);
        var result = logic.GetArticleById(_articleTest.Id);
        Assert.AreEqual(_articleTest, result);
    }

    [TestMethod]
    public void GetArticleByTextValidTest()
    {
        var text = "abo";
        var articles = new List<Article>()
        {
            _articleTest
        };
        var logic = new ArticleLogic(_articleRepoMock.Object, _sessionLogicMock.Object);
        _articleRepoMock.Setup(o => o.GetByText(text)).Returns(articles);
        var result = logic.GetArticleByText(text);
        Assert.AreEqual(articles, result);
    }
    
    [TestMethod]
    public void GetLastTenValidTest()
    {
        var articles = _articles;
        articles.Remove(_articleTest11);
        
        var logic = new ArticleLogic(_articleRepoMock.Object, _sessionLogicMock.Object);
        _articleRepoMock.Setup(o => o.GetLastTen()).Returns(articles);
        var result = logic.GetLastTen();
        Assert.AreEqual(articles, result);
    }

    [TestMethod]
    public void UpdateArticleValidTest()
    {
        var articleUpdated = _articleTest;
        articleUpdated.Title = "New title of articleTest";
        
        var userLogged = _articleTest.Owner;

        var session = new Session()
        {
            Id = Guid.NewGuid(),
            User = userLogged,
            AuthToken = Guid.NewGuid()
        };
        
        var logic = new ArticleLogic(_articleRepoMock.Object, _sessionLogicMock.Object);
        _articleRepoMock.Setup(o => o.GetById(It.IsAny<Expression<Func<Article, bool>>>())).Returns(_articleTest);
        _sessionLogicMock.Setup(o => o.GetLoggedUser(session.AuthToken)).Returns(userLogged);
        _articleRepoMock.Setup(o => o.Update(It.IsAny<Article>()));
        _articleRepoMock.Setup(o => o.Save());
        var result = logic.UpdateArticle(_articleTest.Id, articleUpdated, session.AuthToken);
        Assert.AreEqual(articleUpdated.Comments, result.Comments);
        Assert.AreEqual(articleUpdated.Content, result.Content);
        Assert.AreEqual(articleUpdated.Image, result.Image);
        Assert.AreEqual(articleUpdated.Owner, result.Owner);
        Assert.AreEqual(articleUpdated.Id, result.Id);
        Assert.AreEqual(articleUpdated.Template, result.Template);
        Assert.AreEqual(articleUpdated.Title, result.Title);
        Assert.AreEqual(articleUpdated.DatePublished, result.DatePublished);
        Assert.AreEqual(articleUpdated.IsPublic, result.IsPublic);
        Assert.AreEqual(articleUpdated.DateLastModified, result.DateLastModified);
    }

    [TestMethod]
    public void DeleteSuccessTest()
    {
        var userLogged = _articleTest.Owner;
        
        var session = new Session()
        {
            Id = Guid.NewGuid(),
            User = userLogged,
            AuthToken = Guid.NewGuid()
        };
        
        var logic = new ArticleLogic(_articleRepoMock.Object, _sessionLogicMock.Object);
        _articleRepoMock.Setup(o => o.GetById(It.IsAny<Expression<Func<Article, bool>>>())).Returns(_articleTest);
        _sessionLogicMock.Setup(o => o.GetLoggedUser(session.AuthToken)).Returns(userLogged);
        _articleRepoMock.Setup(o => o.Delete(It.IsAny<Article>()));
        _articleRepoMock.Setup(o => o.Save());
        logic.DeleteArticle(_articleTest.Id, session.AuthToken);
    }
    
}