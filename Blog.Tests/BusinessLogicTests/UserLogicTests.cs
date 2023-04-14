using Blog.BusinessLogic;
using Blog.IDataAccess;
using Blog.Domain.Entities;
using Blog.Domain.Enums;
using Moq;

namespace Blog.Tests.BusinessLogicTests;


[TestClass]
public class UserLogicTests
{
    [TestMethod]
    public void CreateValidUser()
    {
        User user = new User()
        {
            Id = Guid.NewGuid(),
            FirstName = "Nicolas",
            LastName = "Hernandez",
            Username = "NicolasAHF",
            Password = "123456",
            Roles = new List<UserRole>{},
            Email = "nicolas@example.com"
        };

        UserRole role = new UserRole()
        {
            Role = Role.Blogger,
            UserId = user.Id,
            User = user
        };
        
        user.Roles.Add(role);
        
        var mock = new Mock<IRepository<User>>(MockBehavior.Strict);

        var logic = new UserLogic(mock.Object);
        mock.Setup(o => o.Insert(It.IsAny<User>()));
        var result = logic.CreateUser(user);
        mock.VerifyAll();
        Assert.AreEqual(user, result);
    }
}