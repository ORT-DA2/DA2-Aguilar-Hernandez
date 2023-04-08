using Blog.BusinessLogic;
using Blog.BusinessLogic.Exceptions;
using Blog.Domain.Entities;
using Blog.Domain.Enums;
using Blog.WebApi.Controllers;
using Blog.WebApi.Controllers.DTOs;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace Blog.Tests.WebApiTests;

[TestClass]
public class UserControllerTests
{
    [TestMethod]
    public void GetByIdValidUserTest()
    {
        User user = new User()
        {
            Id = new Guid("f2929c98-e6f8-4d48-8d36-9eccb6fe7558"),
            FirstName = "Nicolas",
            LastName = "Hernandez",
            Username = "NicolasAHF",
            Password = "123456",
            Role = Role.Blogger,
            Email = "nicolas@example.com"
        };

        var mock = new Mock<IUserLogic>(MockBehavior.Strict);

        var controller = new UsersController(mock.Object);
        mock.Setup(o => o.GetUserById(user.Id)).Returns(user);
        var result = controller.GetUserById(user.Id);
        var okResult = result as OkObjectResult;
        var dto = okResult.Value as UserDetailDTO;
        mock.VerifyAll();
        Assert.IsTrue(user.Id.Equals(dto.Id));
        Assert.IsTrue(user.FirstName.Equals(dto.FirstName));
        Assert.IsTrue(user.LastName.Equals(dto.LastName));
        Assert.IsTrue(user.Username.Equals(dto.Username));
        Assert.IsTrue(user.Password.Equals(dto.Password));
        Assert.IsTrue(user.Role.Equals(dto.Role));
        Assert.IsTrue(user.Email.Equals(dto.Email));
    }
    
    [TestMethod]
    public void GetByIdNotFoundUserTest()
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

        var mock = new Mock<IUserLogic>(MockBehavior.Strict);

        var controller = new UsersController(mock.Object);
        mock.Setup(o => o.GetUserById(user.Id)).Throws(new NotFoundException("User not found"));
        var result = controller.GetUserById(user.Id);
        mock.VerifyAll();
        Assert.IsInstanceOfType(result, typeof(NotFoundObjectResult));
        
    }
    
    [TestMethod]
    public void GetAllUsersValidTest()
    {
        User user1 = new User()
        {
            Id = Guid.NewGuid(),
            FirstName = "Nicolas",
            LastName = "Hernandez",
            Username = "NicolasAHF",
            Password = "123456",
            Role = Role.Blogger,
            Email = "nicolas@example.com"
        };
        
        User user2 = new User()
        {
            Id = Guid.NewGuid(),
            FirstName = "Francisco",
            LastName = "Aguilar",
            Username = "FAguilar",
            Password = "123456",
            Role = Role.Admin,
            Email = "Francisco@example.com"
        };

        List<User> users = new List<User>()
        {
            user1,
            user2
        };

        var mock = new Mock<IUserLogic>(MockBehavior.Strict);

        var controller = new UsersController(mock.Object);
        mock.Setup(o => o.GetAllUsers()).Returns(users);
        var result = controller.GetAllUsers();
        var okResult = result as OkObjectResult;
        var dto = okResult.Value as List<User>;
        mock.VerifyAll();
        Assert.AreEqual(users, dto);
    }
    
    [TestMethod]
    public void GetAllUsersNotFoundTest()
    {
        var mock = new Mock<IUserLogic>(MockBehavior.Strict);

        var controller = new UsersController(mock.Object);
        mock.Setup(o => o.GetAllUsers()).Throws(new NotFoundException("There are no users."));
        var result = controller.GetAllUsers();
        mock.VerifyAll();
        Assert.IsInstanceOfType(result, typeof(NotFoundObjectResult));
        
    }

    [TestMethod]
    public void CreateValidUser()
    {
        CreateUserDTO userDTO = new CreateUserDTO()
        {
            FirstName = "Nicolas",
            LastName = "Hernandez",
            Username = "NicolasAHF",
            Password = "123456",
            Email = "nicolas@example.com"
        };
        
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

        UserDetailDTO userDetailDto = new UserDetailDTO(user);
        
        var mock = new Mock<IUserLogic>(MockBehavior.Strict);

        var controller = new UsersController(mock.Object);
        mock.Setup(o => o.CreateUser(It.IsAny<User>())).Returns(user);
        var result = controller.CreateUser(userDTO);
        var okResult = result as CreatedResult;
        var dto = okResult.Value as UserDetailDTO;
        mock.VerifyAll();
        Assert.AreEqual(user.FirstName, dto.FirstName);
    }
}