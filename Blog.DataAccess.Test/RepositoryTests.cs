using Blog.Domain.Entities;
using Blog.Domain.Enums;
using Microsoft.EntityFrameworkCore;

namespace Blog.DataAccess.Test;

[TestClass]
public class RepositoryTests
{
    private readonly Repository<User> _repository;
    private readonly BlogDbContext _blogContext;

    public RepositoryTests()
    {
        _blogContext = ContextFactory.GetNewContext(ContextType.SQLite);
        _repository = new Repository<User>(_blogContext);
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
        _blogContext.AddRange(elementsInDatabase);
        _blogContext.SaveChanges();
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
        _blogContext.AddRange(elementsInDatabase);
        _blogContext.SaveChanges();
        var elementExpected = elementsInDatabase.Where(e => e.Id.ToString().Equals("b90af3a0-f9d9-436e-b0c5-52b1f78fc567")).FirstOrDefault();

        var elementSaved = _repository.GetById(e => e.Id.Equals(elementExpected.Id));
        
        Assert.AreEqual(elementExpected, elementSaved);
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

        var elementSaved = _blogContext.Users.FirstOrDefault(u => u.Id == newElement.Id);
        
        Assert.IsNotNull(elementSaved);
        Assert.AreEqual("Francisco", elementSaved.FirstName);
        Assert.AreEqual("Aguilar", elementSaved.LastName);
        Assert.AreEqual("FAguilar", elementSaved.Username);
        Assert.AreEqual("123456", elementSaved.Password);
        Assert.AreEqual(newElement.Roles, elementSaved.Roles);
        Assert.AreEqual("Francisco@example.com", elementSaved.Email);
    }
    
    [TestMethod]
    public void UpdateElement()
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
        _blogContext.AddRange(elementsInDatabase);
        _blogContext.SaveChanges();
        
        var elementExpected = elementsInDatabase.Where(e => e.Id.ToString().Equals("b90af3a0-f9d9-436e-b0c5-52b1f78fc567")).FirstOrDefault();
        
        
        UserRole role = new UserRole()
        {
            Role = Role.Blogger,
            UserId = elementExpected.Id,
            User = elementExpected
        };
        
        elementExpected.Roles.Add(role);
        elementExpected.FirstName = "Nicolas";
        
        _repository.Update(elementExpected);
        _repository.Save();

        var elementSaved = _blogContext.Users.FirstOrDefault(u => u.Id == elementExpected.Id);
        
        Assert.IsNotNull(elementSaved);
        Assert.AreNotEqual("Francisco", elementSaved.FirstName);
        Assert.AreEqual("Hernandez", elementSaved.LastName);
        Assert.AreEqual("NicolasAHF", elementSaved.Username);
        Assert.AreEqual("123456", elementSaved.Password);
        Assert.AreEqual(elementExpected.Roles, elementSaved.Roles);
        Assert.AreEqual("nicolas@example.com", elementSaved.Email);
    }
    
    [TestMethod]
    public void DeleteElement()
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
        _blogContext.AddRange(elementsInDatabase);
        _blogContext.SaveChanges();
        
        var elementExpected = elementsInDatabase.Where(e => e.Id.ToString().Equals("b90af3a0-f9d9-436e-b0c5-52b1f78fc567")).FirstOrDefault();
        
        
        UserRole role = new UserRole()
        {
            Role = Role.Blogger,
            UserId = elementExpected.Id,
            User = elementExpected
        };
        
        elementExpected.Roles.Add(role);
        elementExpected.FirstName = "Nicolas";
        
        _repository.Delete(elementExpected);
        _repository.Save();

        var elementSaved = _blogContext.Users.FirstOrDefault(u => u.Id == elementExpected.Id);
        
        Assert.IsNull(elementSaved);
    }

    [TestMethod]
    public void GetByText()
    {
        
    }
}