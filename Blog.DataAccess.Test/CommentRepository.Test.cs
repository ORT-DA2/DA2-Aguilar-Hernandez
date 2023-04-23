using Blog.Domain.Entities;
using Microsoft.EntityFrameworkCore;

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
    public void GetByIdUser()
    {
        var elementsInDatabase = new List<Comment>
        {
            //TODO 
        };
        _blogContext.AddRange(elementsInDatabase);
        _blogContext.SaveChanges();
        var elementExpected = elementsInDatabase.Where(e => e.Id.ToString().Equals("b90af3a0-f9d9-436e-b0c5-52b1f78fc567")).FirstOrDefault();

        var elementSaved = _userRepository.GetById(e => e.Id.Equals(elementExpected.Id));
        
        Assert.AreEqual(elementExpected, elementSaved);
    }
    
}