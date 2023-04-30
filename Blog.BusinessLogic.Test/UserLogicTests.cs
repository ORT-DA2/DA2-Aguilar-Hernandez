using System.Linq.Expressions;
using Blog.Domain.Entities;
using Blog.Domain.Enums;
using Blog.Domain.Exceptions;
using Blog.IBusinessLogic;
using Blog.IDataAccess;
using Moq;

namespace Blog.BusinessLogic.Test;


[TestClass]
public class UserLogicTests
{
    private Mock<IRepository<User>> _userRepoMock;
    private Mock<ISessionLogic> _sessionLogicMock;

    [TestInitialize]
    public void Setup()
    {
        _userRepoMock = new Mock<IRepository<User>>(MockBehavior.Strict);
        _sessionLogicMock = new Mock<ISessionLogic>(MockBehavior.Strict);
    }

    [TestCleanup]
    public void Cleanup()
    {
        _userRepoMock.VerifyAll();
        _sessionLogicMock.VerifyAll();
    }
    
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
        

        var logic = new UserLogic(_userRepoMock.Object, _sessionLogicMock.Object);
        _userRepoMock.Setup(o => o.Insert(It.IsAny<User>()));
        _userRepoMock.Setup(o => o.Save());
        var result = logic.CreateUser(user);
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
        

        var logic = new UserLogic(_userRepoMock.Object, _sessionLogicMock.Object);
        _userRepoMock.Setup(o => o.GetAll()).Returns(users);
        var result = logic.GetAllUsers();
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
        

        var logic = new UserLogic(mock.Object, _sessionLogicMock.Object);
        mock.Setup(o => o.GetById(It.IsAny<Expression<Func<User, bool>>>())).Returns(user);
        
        var result = logic.GetUserById(user.Id);
        mock.VerifyAll();
        Assert.AreEqual(user, result);
    }
    
    [TestMethod]
    public void UpdateUserValidTestAdmin()
    {
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

        user.Roles = new List<UserRole> { new() { Role = Role.Admin, User = user, UserId = user.Id } };

        User userLogged = user;

        Session session = new Session()
        {
            Id = Guid.NewGuid(),
            User = userLogged,
            AuthToken = Guid.NewGuid()
        };
        
        User userUpdated = new User()
        {
            Id = user.Id,
            FirstName = "Antonio",
            LastName = "Hernandez",
            Username = "NicolasAHF",
            Password = "123456",
            Roles = user.Roles,
            Email = "nicolas@example.com"
        };


        var logic = new UserLogic(_userRepoMock.Object, _sessionLogicMock.Object);
        _userRepoMock.Setup(o => o.GetById(It.IsAny<Expression<Func<User, bool>>>())).Returns(user);
        _sessionLogicMock.Setup(o => o.GetLoggedUser(session.AuthToken)).Returns(userLogged);
        _userRepoMock.Setup(o => o.Update(It.IsAny<User>()));
        _userRepoMock.Setup(o => o.Save());
        var result = logic.UpdateUser(user.Id, userUpdated, session.AuthToken);
        Assert.AreEqual(userUpdated.FirstName, result.FirstName);
        Assert.AreEqual(userUpdated.LastName, result.LastName);
        Assert.AreEqual(userUpdated.Username, result.Username);
        Assert.AreEqual(userUpdated.Password, result.Password);
        Assert.AreEqual(userUpdated.Id, result.Id);
        Assert.AreEqual(userUpdated.Roles, result.Roles);
        Assert.AreEqual(userUpdated.Email, result.Email);
    }
    
    [TestMethod]
    public void UpdateUserValidTestBlogger()
    {
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

        user.Roles = new List<UserRole> { new() { Role = Role.Blogger, User = user, UserId = user.Id } };

        User userLogged = user;

        Session session = new Session()
        {
            Id = Guid.NewGuid(),
            User = userLogged,
            AuthToken = Guid.NewGuid()
        };
        
        User userUpdated = new User()
        {
            Id = user.Id,
            FirstName = "Antonio",
            LastName = "Hernandez",
            Username = "NicolasAHF",
            Password = "123456",
            Roles = user.Roles,
            Email = "nicolas@example.com"
        };


        var logic = new UserLogic(_userRepoMock.Object, _sessionLogicMock.Object);
        _userRepoMock.Setup(o => o.GetById(It.IsAny<Expression<Func<User, bool>>>())).Returns(user);
        _sessionLogicMock.Setup(o => o.GetLoggedUser(session.AuthToken)).Returns(userLogged);
        _userRepoMock.Setup(o => o.Update(It.IsAny<User>()));
        _userRepoMock.Setup(o => o.Save());
        var result = logic.UpdateUser(user.Id, userUpdated, session.AuthToken);
        Assert.AreEqual(userUpdated.FirstName, result.FirstName);
        Assert.AreEqual(userUpdated.LastName, result.LastName);
        Assert.AreEqual(userUpdated.Username, result.Username);
        Assert.AreEqual(userUpdated.Password, result.Password);
        Assert.AreEqual(userUpdated.Id, result.Id);
        Assert.AreEqual(userUpdated.Roles, result.Roles);
        Assert.AreEqual(userUpdated.Email, result.Email);
    }
    
    [TestMethod]
    [ExpectedException(typeof(ArgumentException),
        "You can´t update other user")]
    public void UpdateUserFailTestBlogger()
    {
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

        User userLogged = new User()
        {
            Id = Guid.NewGuid(),
            FirstName = "Francisco",
            LastName = "Aguilar",
            Username = "FranAGui",
            Password = "123456",
            Roles = new List<UserRole>{},
            Email = "nicolas@example.com"
        };
        
        userLogged.Roles = new List<UserRole> { new() { Role = Role.Blogger, User = user, UserId = user.Id } };

        Session session = new Session()
        {
            Id = Guid.NewGuid(),
            User = userLogged,
            AuthToken = Guid.NewGuid()
        };
        
        User userUpdated = new User()
        {
            Id = user.Id,
            FirstName = "Antonio",
            LastName = "Hernandez",
            Username = "NicolasAHF",
            Password = "123456",
            Roles = user.Roles,
            Email = "nicolas@example.com"
        };


        var logic = new UserLogic(_userRepoMock.Object, _sessionLogicMock.Object);
        _userRepoMock.Setup(o => o.GetById(It.IsAny<Expression<Func<User, bool>>>())).Returns(user);
        _sessionLogicMock.Setup(o => o.GetLoggedUser(session.AuthToken)).Returns(userLogged);
        logic.UpdateUser(user.Id, userUpdated, session.AuthToken);
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
        
        user.Roles = new List<UserRole> { new UserRole() { Role = Role.Admin, User = user, UserId = user.Id } };
        
        User userLogged = user;

        Session session = new Session()
        {
            Id = Guid.NewGuid(),
            User = userLogged,
            AuthToken = Guid.NewGuid()
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

        var logic = new UserLogic(_userRepoMock.Object, _sessionLogicMock.Object);
        _userRepoMock.Setup(o => o.GetById(It.IsAny<Expression<Func<User, bool>>>())).Returns((User)null);
        logic.UpdateUser(user.Id, user, session.AuthToken);
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
        

        var logic = new UserLogic(mock.Object, _sessionLogicMock.Object);
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
        

        var logic = new UserLogic(mock.Object, _sessionLogicMock.Object);
        mock.Setup(o => o.GetById(It.IsAny<Expression<Func<User, bool>>>()));
        mock.Setup(o => o.Save());
        
        logic.DeleteUser(user.Id);
        mock.VerifyAll();
    }
}