using Blog.BusinessLogic;
using Blog.DataAccess;
using Blog.Domain.Entities;
using Blog.Domain.Enums;
using Blog.WebApi.Controllers;
using Blog.WebApi.Controllers.DTOs;
using Microsoft.AspNetCore.Mvc;
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
            Role = Role.Blogger,
            Email = "nicolas@example.com"
        };
        
        var mock = new Mock<IUserRepository>(MockBehavior.Strict);

        var logic = new UserLogic(mock.Object);
        mock.Setup(o => o.CreateUser(It.IsAny<User>())).Returns(user);
        var result = logic.CreateUser(user);
        mock.VerifyAll();
        Assert.AreEqual(user, result);
    }
}