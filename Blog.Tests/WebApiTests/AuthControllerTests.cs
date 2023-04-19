﻿using System.Security.Authentication;
using Blog.Domain.Entities;
using Blog.IBusinessLogic;
using Blog.WebApi.Controllers;
using Blog.WebApi.Controllers.DTOs;
using Blog.WebApi.Controllers.DTOs.Auth;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace Blog.Tests.WebApiTests;

[TestClass]
public class AuthControllerTests
{

    [TestMethod]
    public void SuccessfulLoginTest()
    {
        LoginDTO session = new LoginDTO()
        {
            Username = "NicolasAHF",
            Password = "123456"
        };

        Guid token = Guid.NewGuid();
        
        var mock = new Mock<ISessionService>(MockBehavior.Strict);

        var controller = new AuthController(mock.Object);
        mock.Setup(o => o.Login(session.Username, session.Password)).Returns(token);
        var result = controller.Login(session);
        var okResult = result as OkObjectResult;
        var tokenResult = okResult.Value;
        mock.VerifyAll();
        
        Assert.AreEqual(token, tokenResult);
    }
    
    [TestMethod]
    public void LoginFailTest()
    {
        LoginDTO session = new LoginDTO()
        {
            Username = "NicolasAHF",
            Password = "123456"
        };

        var mock = new Mock<ISessionService>(MockBehavior.Strict);

        var controller = new AuthController(mock.Object);
        mock.Setup(o => o.Login(session.Username, session.Password)).Throws(new InvalidCredentialException());
        var result = controller.Login(session);
        var unauthorizedObjectResultResult = result as UnauthorizedObjectResult;
        mock.VerifyAll();
        Assert.IsInstanceOfType(result, typeof(UnauthorizedObjectResult));
    }
    
    [TestMethod]
    public void SuccessfulLogoutTest()
    {
        
        Guid token = Guid.NewGuid();

        var mock = new Mock<ISessionService>(MockBehavior.Strict);

        var controller = new AuthController(mock.Object);
        mock.Setup(o => o.Logout());
        var result = controller.Logout(token);
        var okResult = result as OkResult;
        mock.VerifyAll();
        
        Assert.IsInstanceOfType(result, typeof(OkResult));
    }
}