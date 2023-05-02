using System.Security.Authentication;
using Blog.Domain.Entities;
using Blog.IBusinessLogic;
using Blog.Models.In.Auth;
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
        _sessionMock = new Mock<ISessionLogic>();
    }

    [TestCleanup]
    public void Cleanup()
    {
        _sessionMock.VerifyAll();
    }

    [TestMethod]
    public void SuccessfulLoginTest()
    {
        LoginDTO session = new LoginDTO()
        {
            Username = "NicolasAHF",
            Password = "123456"
        };

        Guid token = Guid.NewGuid();

        var notificationLogic = new Mock<INotificationLogic>();
        var controller = new AuthController(_sessionMock.Object, notificationLogic.Object);
        _sessionMock.Setup(o => o.Login(session.Username, session.Password)).Returns(token);
        var result = controller.Login(session);
        var okResult = result as OkObjectResult;
        var tokenResult = okResult.Value.ToString();
        Notification[] notifications = new Notification[1];
        var expected = new { token = token, notifications = notifications };
        
        Assert.AreEqual(expected.ToString(), tokenResult);
    }
    
    [TestMethod]
    public void LoginFailTest()
    {
        LoginDTO session = new LoginDTO()
        {
            Username = "NicolasAHF",
            Password = "123456"
        };

        var notificationLogic = new Mock<INotificationLogic>();
        var controller = new AuthController(_sessionMock.Object, notificationLogic.Object);
        _sessionMock.Setup(o => o.Login(session.Username, session.Password)).Throws(new InvalidCredentialException());
        var result = controller.Login(session);
        Assert.IsInstanceOfType(result, typeof(UnauthorizedObjectResult));
    }
    
    [TestMethod]
    public void SuccessfulLogoutTest()
    {
        
        Guid token = Guid.NewGuid();

        var notificationLogic = new Mock<INotificationLogic>();
        var controller = new AuthController(_sessionMock.Object, notificationLogic.Object);
        _sessionMock.Setup(o => o.Logout(token));
        var result = controller.Logout(token);
        
        _sessionMock.VerifyAll();
        
        Assert.IsInstanceOfType(result, typeof(OkResult));
    }
}