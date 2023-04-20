using Blog.DataAccess;
using Blog.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Blog.Tests.DataAccessTests;

[TestClass]
public class UserRepositoryTests
{
    private readonly UserRepository _userRepository;
    private readonly BlogDbContext _blogContext;

    public UserRepositoryTests()
    {
        _blogContext = ContextFactory.GetNewContext(ContextType.SQLite);
        _userRepository = new UserRepository(_blogContext);
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
    public void GetByIdUser()
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

        var elementSaved = _userRepository.GetById(e => e.Id.Equals(elementExpected.Id));
        
        Assert.AreEqual(elementExpected, elementSaved);
    }
}