using System.Security.Authentication;
using Blog.Domain.Entities;
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
        
        _sessionMock.VerifyAll();
        
        Assert.IsInstanceOfType(result, typeof(OkResult));
    }
}