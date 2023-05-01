using System.Security.Authentication;
using Blog.Domain.Entities;
using Blog.Domain.Enums;
using Blog.IBusinessLogic;
using Blog.Models.In;
using Blog.WebApi.Controllers;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace Blog.Tests.WebApiTests;

[TestClass]
public class AuthControllerTests
{
    private Mock<ISessionLogic> _sessionMock;
    
    [TestInitialize]
    public void Setup()
    {
        _sessionMock = new Mock<ISessionLogic>(MockBehavior.Strict);
    }

    [TestCleanup]
    public void Cleanup()
    {
        _sessionMock.VerifyAll();
    }

    [TestMethod]
    public void SuccessfulLoginTest()
    {
        LoginDto session = new LoginDto()
        {
            Username = "NicolasAHF",
            Password = "123456"
        };

        Guid token = Guid.NewGuid();
        

        var controller = new AuthController(_sessionMock.Object);
        _sessionMock.Setup(o => o.Login(session.Username, session.Password)).Returns(token);
        var result = controller.Login(session);
        var okResult = result as OkObjectResult;
        var tokenResult = okResult.Value.ToString();
        var expected = new { token = token };
        
        Assert.AreEqual(expected.ToString(), tokenResult);
    }
    
    [TestMethod]
    [ExpectedException(typeof(InvalidCredentialException))]
    public void LoginFailTest()
    {
        LoginDto session = new LoginDto()
        {
            Username = "NicolasAHF",
            Password = "123456"
        };

        var controller = new AuthController(_sessionMock.Object);
        _sessionMock.Setup(o => o.Login(session.Username, session.Password)).Throws(new InvalidCredentialException());
        controller.Login(session);
    }
    
    [TestMethod]
    public void SuccessfulLogoutTest()
    {
        
        Guid token = Guid.NewGuid();

        var controller = new AuthController(_sessionMock.Object);
        _sessionMock.Setup(o => o.Logout(token));
        var result = controller.Logout(token);
        var okResult = result as OkObjectResult;
        var value = okResult.Value.ToString();
        _sessionMock.VerifyAll();
        
        Assert.AreEqual(value, "Logout successfuly");
        
    }
    
    [TestMethod]
    public void SuccessfulRegisterTest()
    {
        RegisterDto session = new RegisterDto()
        {
            FirstName = "Nicolas",
            LastName = "Hernandez",
            Username = "NicolasAHF",
            Password = "123456",
            Email = "nicolas@example.com"
        };

        var newUser = new User()
        {
            Id = Guid.NewGuid(),
            FirstName = "Nicolas",
            LastName = "Hernandez",
            Username = "NicolasAHF",
            Password = "123456",
            Roles = new List<UserRole>{},
            Email = "nicolas@example.com"
        };
        

        var controller = new AuthController(_sessionMock.Object);
        _sessionMock.Setup(o => o.Register(It.IsAny<User>())).Returns(newUser);
        var result = controller.Register(session);
        var okResult = result as OkObjectResult;
        var userResult = okResult.Value;

        Assert.AreEqual(newUser, userResult);
    }
}