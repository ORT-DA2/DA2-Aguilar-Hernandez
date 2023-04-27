﻿using System.Linq.Expressions;
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
    private Mock<ArticleRepository> _articleRepoMock;
    private Article _articleTest;
    private List<Article> _articles;

    [TestInitialize]
    public void Setup()
    {
        _articleRepoMock = new Mock<ArticleRepository>(MockBehavior.Strict);
        
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

        _articles = new List<Article>()
        {
            _articleTest
        };

    }

    [TestCleanup]
    public void Cleanup()
    {
        _articleRepoMock.VerifyAll();
    }

    [TestMethod]
    public void CreateArticleSuccessTest()
    {
        var logic = new ArticleLogic(_articleRepoMock.Object);
        _articleRepoMock.Setup(o => o.Insert(It.IsAny<Article>()));
        _articleRepoMock.Setup(o => o.Save());
        var result = logic.CreateArticle(_articleTest);
        Assert.AreEqual(_articleTest, result);
    }
    
    [TestMethod]
    public void GetAllArticlesValidTest()
    {
        var logic = new ArticleLogic(_articleRepoMock.Object);
        _articleRepoMock.Setup(o => o.GetAll()).Returns(_articles);
        var result = logic.GetAllArticles();
        Assert.AreEqual(_articles.Count(), result.Count());
    }
    
    [TestMethod]
    public void GetArticleByIdValidTest()
    {
        var logic = new ArticleLogic(_articleRepoMock.Object);
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
        var logic = new ArticleLogic(_articleRepoMock.Object);
        _articleRepoMock.Setup(o => o.GetByText(text)).Returns(articles);
        var result = logic.GetArticleByText(text);
        Assert.AreEqual(_articleTest, result);
    }
    
}