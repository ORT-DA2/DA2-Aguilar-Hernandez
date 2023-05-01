﻿using System.Linq.Expressions;
using System.Security.Authentication;
using Blog.Domain.Entities;
using Blog.Domain.Enums;
using Blog.IBusinessLogic;
using Blog.IDataAccess;
using Moq;

namespace Blog.BusinessLogic.Test;

[TestClass]
public class SessionLogicTests
{
    [TestMethod]
    public void SuccessfulLoginTest()
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
        

        var mockSession = new Mock<IRepository<Session>>(MockBehavior.Strict);
        var mockUser = new Mock<IRepository<User>>(MockBehavior.Strict);
        var mockUserLogic = new Mock<IUserLogic>(MockBehavior.Strict);

        var logic = new SessionLogic(mockSession.Object, mockUser.Object, mockUserLogic.Object);
        mockUser.Setup(o => o.GetBy(It.IsAny<Expression<Func<User, bool>>>())).Returns(user);
        mockSession.Setup(o => o.Insert(It.IsAny<Session>()));
        mockSession.Setup(o => o.Save());
        var result = logic.Login(user.Email, user.Password);
        mockSession.VerifyAll();
        mockUser.VerifyAll();
        Assert.IsInstanceOfType(result, typeof(Guid));
    }
    
    [TestMethod]
    [ExpectedException(typeof(InvalidCredentialException), "Invalid credentials")]
    public void LoginInvalidTest()
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
        

        var mockSession = new Mock<IRepository<Session>>(MockBehavior.Strict);
        var mockUser = new Mock<IRepository<User>>(MockBehavior.Strict);
        var mockUserLogic = new Mock<IUserLogic>(MockBehavior.Strict);

        var logic = new SessionLogic(mockSession.Object, mockUser.Object, mockUserLogic.Object);
        mockUser.Setup(o => o.GetBy(It.IsAny<Expression<Func<User, bool>>>())).Returns((User)null);
        var result = logic.Login(user.Email, user.Password);
        mockSession.VerifyAll();
        mockUser.VerifyAll();
    }
    
    [TestMethod]
    public void SuccessfulGetLoggedUserTest()
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

        Session session = new Session()
        {
            Id = Guid.NewGuid(),
            User = user,
        };
        

        var mockSession = new Mock<IRepository<Session>>(MockBehavior.Strict);
        var mockUser = new Mock<IRepository<User>>(MockBehavior.Strict);
        var mockUserLogic = new Mock<IUserLogic>(MockBehavior.Strict);

        var logic = new SessionLogic(mockSession.Object, mockUser.Object, mockUserLogic.Object);
        mockSession.Setup(o => o.GetBy(It.IsAny<Expression<Func<Session, bool>>>())).Returns(session);
        var result = logic.GetLoggedUser(session.AuthToken);
        mockSession.VerifyAll();
        Assert.AreEqual(user, result);
    }
    
    [TestMethod]
    [ExpectedException(typeof(KeyNotFoundException), "User not found")]
    public void GetLoggedUserFailTest()
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

        Session session = new Session()
        {
            Id = Guid.NewGuid(),
            User = user,
        };
        

        var mockSession = new Mock<IRepository<Session>>(MockBehavior.Strict);
        var mockUser = new Mock<IRepository<User>>(MockBehavior.Strict);
        var mockUserLogic = new Mock<IUserLogic>(MockBehavior.Strict);

        var logic = new SessionLogic(mockSession.Object, mockUser.Object, mockUserLogic.Object);
        mockSession.Setup(o => o.GetBy(It.IsAny<Expression<Func<Session, bool>>>())).Returns((Session)null);
        var result = logic.GetLoggedUser(session.AuthToken);
        mockSession.VerifyAll();
    }
    
    [TestMethod]
    public void SuccessfulLogoutTest()
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

        Session session = new Session()
        {
            Id = Guid.NewGuid(),
            User = user,
        };
        

        var mockSession = new Mock<IRepository<Session>>(MockBehavior.Strict);
        var mockUser = new Mock<IRepository<User>>(MockBehavior.Strict);
        var mockUserLogic = new Mock<IUserLogic>(MockBehavior.Strict);

        var logic = new SessionLogic(mockSession.Object, mockUser.Object, mockUserLogic.Object);
        mockSession.Setup(o => o.GetBy(It.IsAny<Expression<Func<Session, bool>>>())).Returns(session);
        mockSession.Setup(o => o.Delete(session));
        mockSession.Setup(o => o.Save());
        logic.Logout(session.AuthToken);
        mockSession.VerifyAll();
    }
    
    [TestMethod]
    public void RegisterValidUser()
    {
        User user = new User()
        {
            Id = Guid.NewGuid(),
            FirstName = "Nicolas",
            LastName = "Hernandez",
            Username = "NicolasAHF",
            Password = "123456",
            Roles = new List<UserRole>{},
            Email = "nicolastest@example.com"
        };

        UserRole role = new UserRole()
        {
            Role = Role.Blogger,
            UserId = user.Id,
            User = user
        };
        
        user.Roles.Add(role);
        var token = Guid.NewGuid();
        
        var mockSession = new Mock<IRepository<Session>>(MockBehavior.Strict);
        var mockUser = new Mock<IRepository<User>>(MockBehavior.Strict);
        var mockUserLogic = new Mock<IUserLogic>(MockBehavior.Strict);

        var logic = new SessionLogic(mockSession.Object, mockUser.Object, mockUserLogic.Object);
        mockUser.Setup(s => s.GetBy(It.IsAny<Expression<Func<User, bool>>>())).Returns((User)null);
        mockUserLogic.Setup(o => o.UserAlreadyExist(It.IsAny<User>()));
        mockUserLogic.Setup(o => o.ValidateNull(It.IsAny<User>()));
        mockUserLogic.Setup(o => o.GeneralValidation(It.IsAny<User>()));
        mockUser.Setup(o => o.Insert(It.IsAny<User>()));
        mockUser.Setup(o => o.Save());
        var result = logic.Register(user);
        Assert.AreEqual(user, result);
    }
}