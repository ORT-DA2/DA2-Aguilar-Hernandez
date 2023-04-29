using Blog.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace Blog.DataAccess.Test;

[TestClass]
public class CommentRepositoryTest
{
    private readonly UserRepository _userRepository;
    private readonly CommentRepository _commentRepository;
    private readonly BlogDbContext _blogContext;

    public CommentRepositoryTest()
    {
        _blogContext = ContextFactory.GetNewContext(ContextType.SQLite);
        _userRepository = new UserRepository(_blogContext);
        _commentRepository = new CommentRepository(_blogContext);
    }

    [TestInitialize]
    public void SetUp()
    {
        _blogContext.Database.OpenConnection();
        _blogContext.Database.EnsureCreated();
    }

    [TestCleanup]
    public void CleanUp()
    {
        _blogContext.Database.EnsureDeleted();
    }
    
    [TestMethod]
    public void GetCommentById()
    {
        User nicolas = new User()
        {
            Id = new Guid("b90af3a0-f9d9-436e-b0c5-52b1f78fc567"),
            FirstName = "Nicolas",
            LastName = "Hernandez",
            Username = "NicolasAHF",
            Password = "123456",
            Roles = new List<UserRole> { },
            Email = "nicolas@example.com"
        };
        Article article = new Article()
        {
            Title = "title",
            Content = "Post",
            DatePublished = DateTime.Now,
            DateLastModified = DateTime.Now,
            Id = Guid.NewGuid(),
            Image = "image.jpg",
            IsPublic = true,
            Owner = nicolas
        };
        var elementsInDatabase = new List<Comment>
        {
            new Comment()
            {
                Id = new Guid("b90af3a0-f9d9-436e-b0c5-52b1f78fc567"),
                Body = "Buen post",
                Article = article,
                Owner = nicolas,
                Reply = "Gracias capo"
            },
            new Comment()
            {
                Id = Guid.NewGuid(),
                Body = "Buen post",
                Article = article,
                Owner = nicolas,
                Reply = "Gracias capo"
            }
        };
        _blogContext.AddRange(elementsInDatabase);
        _blogContext.SaveChanges();
        var elementExpected = elementsInDatabase.FirstOrDefault(e => e.Id.ToString().Equals("b90af3a0-f9d9-436e-b0c5-52b1f78fc567"));

        var elementSaved = _commentRepository.GetById(e => e.Id.Equals(elementExpected.Id));
        
        Assert.AreEqual(elementExpected, elementSaved);
    }
    
}