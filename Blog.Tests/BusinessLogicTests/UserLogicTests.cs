using System.Linq.Expressions;
using Blog.BusinessLogic;
using Blog.BusinessLogic.Exceptions;
using Blog.DataAccess;
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
        mock.Setup(o => o.Save());
        var result = logic.CreateUser(user);
        mock.VerifyAll();
        Assert.AreEqual(user, result);
    }
    
    [TestMethod]
    public void GetAllUsersValidTest()
    {
        IEnumerable<User> users = new List<User>()
        {
            new User()
            {
                Id = Guid.NewGuid(),
                FirstName = "Nicolas",
                LastName = "Hernandez",
                Username = "NicolasAHF",
                Password = "123456",
                Roles = new List<UserRole> { },
                Email = "nicolas@example.com"
            }
        };

        UserRole role = new UserRole()
        {
            Role = Role.Blogger,
            UserId = users.FirstOrDefault().Id,
            User = users.FirstOrDefault()
        };
        
        users.FirstOrDefault().Roles.Add(role);
        
        var mock = new Mock<IRepository<User>>(MockBehavior.Strict);

        var logic = new UserLogic(mock.Object);
        mock.Setup(o => o.GetAll()).Returns(users);
        var result = logic.GetAllUsers();
        mock.VerifyAll();
        Assert.AreEqual(users.Count(), result.Count());
    }
    
    [TestMethod]
    public void GetUserByIdValidTest()
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

        var mock = new Mock<IRepository<User>>(MockBehavior.Loose);
        

        var logic = new UserLogic(mock.Object);
        mock.Setup(o => o.GetById(It.IsAny<Expression<Func<User, bool>>>())).Returns(user);
        
        var result = logic.GetUserById(user.Id);
        mock.VerifyAll();
        Assert.AreEqual(user, result);
    }
    
    [TestMethod]
    public void UpdateUserValidTest()
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
        
        User userUpdated = new User()
        {
            Id = user.Id,
            FirstName = "Antonio",
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
        userUpdated.Roles.Add(role);
        
        var mock = new Mock<IRepository<User>>(MockBehavior.Strict);

        var logic = new UserLogic(mock.Object);
        mock.Setup(o => o.GetById(It.IsAny<Expression<Func<User, bool>>>())).Returns(user);
        mock.Setup(o => o.Update(It.IsAny<User>()));
        mock.Setup(o => o.Save());
        var result = logic.UpdateUser(user.Id, userUpdated);
        mock.VerifyAll();
        Assert.AreEqual(userUpdated.FirstName, result.FirstName);
        Assert.AreEqual(userUpdated.LastName, result.LastName);
        Assert.AreEqual(userUpdated.Username, result.Username);
        Assert.AreEqual(userUpdated.Password, result.Password);
        Assert.AreEqual(userUpdated.Id, result.Id);
        Assert.AreEqual(userUpdated.Roles, result.Roles);
        Assert.AreEqual(userUpdated.Email, result.Email);
    }
    
    [TestMethod]
    [ExpectedException(typeof(NotFoundException),
        "The user was not found")]
    public void UpdateUserNullTest()
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
        
        User userUpdated = new User()
        {
            Id = user.Id,
            FirstName = "Antonio",
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
        userUpdated.Roles.Add(role);
        
        var mock = new Mock<IRepository<User>>(MockBehavior.Strict);

        var logic = new UserLogic(mock.Object);
        mock.Setup(o => o.GetById(It.IsAny<Expression<Func<User, bool>>>())).Returns((User)null);
        mock.Setup(o => o.Update(It.IsAny<User>()));
        mock.Setup(o => o.Save());
        var result = logic.UpdateUser(user.Id, user);
        mock.VerifyAll();
    }
    
    [TestMethod]
    public void DeleteValidTest()
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

        var mock = new Mock<IRepository<User>>(MockBehavior.Loose);
        

        var logic = new UserLogic(mock.Object);
        mock.Setup(o => o.GetById(It.IsAny<Expression<Func<User, bool>>>())).Returns(user);
        mock.Setup(o => o.Save());
        
        logic.DeleteUser(user.Id);
        mock.VerifyAll();
    }
    
    [TestMethod]
    [ExpectedException(typeof(NotFoundException),
        "The user was not found")]
    public void DeleteNullTest()
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

        var mock = new Mock<IRepository<User>>(MockBehavior.Loose);
        

        var logic = new UserLogic(mock.Object);
        mock.Setup(o => o.GetById(It.IsAny<Expression<Func<User, bool>>>()));
        mock.Setup(o => o.Save());
        
        logic.DeleteUser(user.Id);
        mock.VerifyAll();
    }
}