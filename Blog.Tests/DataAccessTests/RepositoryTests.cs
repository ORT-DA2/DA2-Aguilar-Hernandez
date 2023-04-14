using Blog.DataAccess;
using Blog.Domain;
using Blog.Domain.Entities;
using Blog.Domain.Enums;
using Microsoft.EntityFrameworkCore;

namespace Blog.Tests.DataAccessTests;

[TestClass]
public class RepositoryTests
{
    private readonly Repository<User> _repository;
    private readonly BlogDbContext _BlogContext;

    public RepositoryTests()
    {
        _BlogContext = ContextFactory.GetNewContext(ContextType.SQLite);
        _repository = new Repository<User>(_BlogContext);
    }

    [TestInitialize]
    public void SetUp()
    {
        _BlogContext.Database.OpenConnection();
        _BlogContext.Database.EnsureCreated();
    }

    [TestCleanup]
    public void CleanUp()
    {
        _BlogContext.Database.EnsureDeleted();
    }

    [TestMethod]
    public void GetAll()
    {
        var elementsInDatabase = new List<User>
        {
            new User()
            {
                Id = Guid.NewGuid(),
                FirstName = "Nicolas",
                LastName = "Hernandez",
                Username = "NicolasAHF",
                Password = "123456",
                Roles = new List<UserRole>{},
                Email = "nicolas@example.com"
            },

            new User()
            {
                Id = Guid.NewGuid(),
                FirstName = "Francisco",
                LastName = "Aguilar",
                Username = "FAguilar",
                Password = "123456",
                Roles = new List<UserRole>{},
                Email = "Francisco@example.com"
            }
        };
        _BlogContext.AddRange(elementsInDatabase);
        _BlogContext.SaveChanges();
        var elementsExpected = elementsInDatabase;

        var elementsSaved = _repository.GetAll();
        
        Assert.AreEqual(elementsExpected.Count(), elementsSaved.Count());
    }

    [TestMethod]
    public void GetByIdElement()
    {
        var elementsInDatabase = new List<User>
        {
            new User()
            {
                Id = new Guid("b90af3a0-f9d9-436e-b0c5-52b1f78fc567"),
                FirstName = "Nicolas",
                LastName = "Hernandez",
                Username = "NicolasAHF",
                Password = "123456",
                Roles = new List<UserRole>{},
                Email = "nicolas@example.com"
            },

            new User()
            {
                Id = Guid.NewGuid(),
                FirstName = "Francisco",
                LastName = "Aguilar",
                Username = "FAguilar",
                Password = "123456",
                Roles = new List<UserRole>{},
                Email = "Francisco@example.com"
            }
        };
        _BlogContext.AddRange(elementsInDatabase);
        _BlogContext.SaveChanges();
        var elementsExpected = elementsInDatabase.Where(e => e.Id.Equals("b90af3a0-f9d9-436e-b0c5-52b1f78fc567")).FirstOrDefault();

        var elementsSaved = _repository.GetById(e => e.Id.Equals("b90af3a0-f9d9-436e-b0c5-52b1f78fc567"));
        
        Assert.AreEqual(elementsExpected, elementsSaved);
    }

    [TestMethod]
    public void InsertElement()
    {
        var newElement = new User()
        {
            Id = Guid.NewGuid(),
            FirstName = "Francisco",
            LastName = "Aguilar",
            Username = "FAguilar",
            Password = "123456",
            Roles = new List<UserRole>{},
            Email = "Francisco@example.com"
        };
        
        UserRole role = new UserRole()
        {
            Role = Role.Blogger,
            UserId = newElement.Id,
            User = newElement
        };
        
        newElement.Roles.Add(role);
        
        _repository.Insert(newElement);
        _repository.Save();

        var elementSaved = _BlogContext.Users.FirstOrDefault(u => u.Id == newElement.Id);
        
        Assert.IsNotNull(elementSaved);
        Assert.AreEqual("Francisco", elementSaved.FirstName);
        Assert.AreEqual("Aguilar", elementSaved.LastName);
        Assert.AreEqual("FAguilar", elementSaved.Username);
        Assert.AreEqual("123456", elementSaved.Password);
        Assert.AreEqual(newElement.Roles, elementSaved.Roles);
        Assert.AreEqual("Francisco@example.com", elementSaved.Email);
    }
}