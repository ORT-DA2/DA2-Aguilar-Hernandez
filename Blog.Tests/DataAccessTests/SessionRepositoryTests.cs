using Blog.DataAccess;
using Blog.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Blog.Tests.DataAccessTests;

[TestClass]
public class SessionRepositoryTests
{
    private readonly UserRepository _userRepository;
    private readonly SessionRepository _sessionRepository;
    private readonly BlogDbContext _blogContext;

    public SessionRepositoryTests()
    {
        _blogContext = ContextFactory.GetNewContext(ContextType.SQLite);
        _userRepository = new UserRepository(_blogContext);
        _sessionRepository = new SessionRepository(_blogContext);
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
        var elementsInDatabase = new List<Session>
        {
            new Session()
            {
                AuthToken = new Guid("b90af3a0-f9d9-436e-b0c5-52b1f78fc567"),
                User = new User()
                {
                    Id = Guid.NewGuid(),
                    FirstName = "Nicolas",
                    LastName = "Hernandez",
                    Username = "NicolasAHF",
                    Password = "123456",
                    Roles = new List<UserRole>{},
                    Email = "nicolas@example.com"
                },
                Id = Guid.NewGuid()
            },
        };
        _blogContext.AddRange(elementsInDatabase);
        _blogContext.SaveChanges();
        var elementExpected = elementsInDatabase.Where(e => e.AuthToken.ToString().Equals("b90af3a0-f9d9-436e-b0c5-52b1f78fc567")).FirstOrDefault();

        var elementSaved = _sessionRepository.GetById(s => s.User.Username.Equals(elementExpected.User.Username) && s.User.Password.Equals(elementExpected.User.Password));
        
        Assert.AreEqual(elementExpected, elementSaved);
    }
}