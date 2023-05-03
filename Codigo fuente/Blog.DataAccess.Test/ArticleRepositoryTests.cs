using Blog.Domain.Entities;
using Blog.Domain.Enums;
using Microsoft.EntityFrameworkCore;

namespace Blog.DataAccess.Test;

[TestClass]
public class ArticleRepositoryTests
{
    private readonly ArticleRepository _articleRepository;
    private readonly BlogDbContext _blogContext;
    private List<Article> _elementsInDatabase;
    private User _user;

    public ArticleRepositoryTests()
    {
        _blogContext = ContextFactory.GetNewContext(ContextType.SQLite);
        _articleRepository = new ArticleRepository(_blogContext);
    }

    [TestInitialize]
    public void SetUp()
    {
        _blogContext.Database.OpenConnection();
        _blogContext.Database.EnsureCreated();

        _user = new User()
        {
            Id = Guid.NewGuid(),
            FirstName = "Francisco",
            LastName = "Aguilar",
            Username = "FAguilar",
            Password = "123456",
            Roles = new List<UserRole> { },
            Email = "Francisco@example.com"
        };
        
        _elementsInDatabase = new List<Article>
        {
            new Article()
            {
                Id = new Guid("b90af3a0-f9d9-436e-b0c5-52b1f78fc567"),
                Title = "Test11",
                Content = "Uruguay is a country in south america",
                Owner = _user,
                Comments = new List<Comment>(){},
                DateLastModified = DateTime.Now,
                DatePublished = DateTime.Now,
                Image = "test.jpg",
                IsPublic = true,
                Template = Template.RectangleTop
            
            },
            new Article()
            {
                Id = Guid.NewGuid(),
                Title = "Test11",
                Content = "Uruguay is a country in south america",
                Owner = _user,
                Comments = new List<Comment>(){},
                DateLastModified = DateTime.Now,
                DatePublished = DateTime.Now,
                Image = "test.jpg",
                IsPublic = true,
                Template = Template.RectangleTop
            
            },
            new Article()
            {
                Id = Guid.NewGuid(),
                Title = "Test11",
                Content = "Uruguay is a country in south america",
                Owner = _user,
                Comments = new List<Comment>(){},
                DateLastModified = DateTime.Now,
                DatePublished = DateTime.Now,
                Image = "test.jpg",
                IsPublic = true,
                Template = Template.RectangleTop
            
            },
            new Article()
            {
                Id = Guid.NewGuid(),
                Title = "Test11",
                Content = "Uruguay is a country in south america",
                Owner = _user,
                Comments = new List<Comment>(){},
                DateLastModified = DateTime.Now,
                DatePublished = DateTime.Now,
                Image = "test.jpg",
                IsPublic = true,
                Template = Template.RectangleTop
            
            },
            new Article()
            {
                Id = Guid.NewGuid(),
                Title = "Test11",
                Content = "Uruguay is a country in south america",
                Owner = _user,
                Comments = new List<Comment>(){},
                DateLastModified = DateTime.Now,
                DatePublished = DateTime.Now,
                Image = "test.jpg",
                IsPublic = true,
                Template = Template.RectangleTop
            
            },
            new Article()
            {
                Id = Guid.NewGuid(),
                Title = "Test11",
                Content = "Uruguay is a country in south america",
                Owner = _user,
                Comments = new List<Comment>(){},
                DateLastModified = DateTime.Now,
                DatePublished = DateTime.Now,
                Image = "test.jpg",
                IsPublic = true,
                Template = Template.RectangleTop
            
            },
            new Article()
            {
                Id = Guid.NewGuid(),
                Title = "Test11",
                Content = "Uruguay is a country in south america",
                Owner = _user,
                Comments = new List<Comment>(){},
                DateLastModified = DateTime.Now,
                DatePublished = DateTime.Now,
                Image = "test.jpg",
                IsPublic = true,
                Template = Template.RectangleTop
            
            },
            new Article()
            {
                Id = Guid.NewGuid(),
                Title = "Test11",
                Content = "Uruguay is a country in south america",
                Owner = _user,
                Comments = new List<Comment>(){},
                DateLastModified = DateTime.Now,
                DatePublished = DateTime.Now,
                Image = "test.jpg",
                IsPublic = true,
                Template = Template.RectangleTop
            
            },
            new Article()
            {
                Id = Guid.NewGuid(),
                Title = "Test11",
                Content = "Uruguay is a country in south america",
                Owner = _user,
                Comments = new List<Comment>(){},
                DateLastModified = DateTime.Now,
                DatePublished = DateTime.Now,
                Image = "test.jpg",
                IsPublic = true,
                Template = Template.RectangleTop
            
            },
            new Article()
            {
                Id = Guid.NewGuid(),
                Title = "Test11",
                Content = "Uruguay is a country in south america",
                Owner = _user,
                Comments = new List<Comment>(){},
                DateLastModified = DateTime.Now,
                DatePublished = DateTime.Now,
                Image = "test.jpg",
                IsPublic = true,
                Template = Template.RectangleTop
            
            },
            new Article()
            {
                Id = Guid.NewGuid(),
                Title = "Test11private",
                Content = "Uruguay is a country in south america",
                Owner = _user,
                Comments = new List<Comment>(){},
                DateLastModified = DateTime.Now,
                DatePublished = DateTime.Now,
                Image = "test.jpg",
                IsPublic = false,
                Template = Template.RectangleTop
            
            },
            new Article()
            {
                Id = Guid.NewGuid(),
                Title = "Test11private",
                Content = "Uruguay is a country in south america",
                Owner = _user,
                Comments = new List<Comment>(){},
                DateLastModified = DateTime.Now,
                DatePublished = DateTime.Now,
                Image = "test.jpg",
                IsPublic = false,
                Template = Template.RectangleTop
            
            },
            new Article()
            {
                Id = Guid.NewGuid(),
                Title = "Test11private",
                Content = "Uruguay is a country in south america",
                Owner = _user,
                Comments = new List<Comment>(){},
                DateLastModified = DateTime.Now,
                DatePublished = DateTime.Now,
                Image = "test.jpg",
                IsPublic = true,
                Template = Template.RectangleTop
            
            }
        };
        _blogContext.AddRange(_elementsInDatabase);
        _blogContext.SaveChanges();
    }

    [TestCleanup]
    public void CleanUp()
    {
        _blogContext.Database.EnsureDeleted();
    }
    
    [TestMethod]
    public void GetByAllUser()
    {
        var elementsExpected = _elementsInDatabase;

        var elementsSaved = _articleRepository.GetAll();
        
        Assert.AreEqual(elementsExpected.Count, elementsSaved.Count());
    }
    
    [TestMethod]
    public void GetByArticle()
    {
        var elementExpected = _elementsInDatabase.Where(e => e.Id.ToString().Equals("b90af3a0-f9d9-436e-b0c5-52b1f78fc567")).FirstOrDefault();

        var elementSaved = _articleRepository.GetBy(e => e.Id.Equals(elementExpected.Id));
        
        Assert.AreEqual(elementExpected, elementSaved);
    }
    
    [TestMethod]
    public void GetByText()
    {
        var elementExpected = _elementsInDatabase.Where(e => e.Content.Contains("uru") || e.Title.Contains("uru"));

        var elementSaved = _articleRepository.GetByText("uru");
        
        Assert.AreEqual(elementExpected.Count(), elementSaved.Count());
    }
    
    [TestMethod]
    public void GetLastTen()
    {
        var elementExpected = _elementsInDatabase.OrderByDescending(a => a.DatePublished).Where(a => a.IsPublic == true).Take(10);

        var elementSaved = _articleRepository.GetLastTen();
        
        Assert.AreEqual(elementExpected.Count(), 10);
        Assert.AreEqual(elementExpected.Count(), elementSaved.Count());
    }
    
    [TestMethod]
    public void GetAllpublic()
    {
        var elementExpected = _elementsInDatabase.Where(a => a.IsPublic == true);

        var elementSaved = _articleRepository.GetPublicAll();
        
        Assert.AreEqual(elementExpected.Count(), elementSaved.Count());
    }
    
    [TestMethod]
    public void GetUserArticles()
    {
        var elementExpected = _elementsInDatabase.Where(a => a.Owner.Username == _user.Username);

        var elementSaved = _articleRepository.GetUserArticles(_user.Username);
        
        Assert.AreEqual(elementExpected.Count(), elementSaved.Count());
    }
}