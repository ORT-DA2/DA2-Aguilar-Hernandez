using Blog.BusinessLogic;
using Blog.BusinessLogic.Exceptions;
using Blog.Domain;
using Blog.Domain.Entities;
using Blog.Domain.Enums;
using Blog.IBusinessLogic;
using Blog.WebApi.Controllers;
using Blog.WebApi.Controllers.DTOs;
using Blog.WebApi.Controllers.DTOs.UserRole;
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
            Roles = new List<UserRole>{},
            Email = "nicolas@example.com"
        };
        
        UserRoleBasicInfoDTO role = new UserRoleBasicInfoDTO()
        {
            Role = Role.Blogger,
        };
        
        user.Roles.Add(role.ToEntity());

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
        Assert.AreEqual(user.Roles.Count, dto.Roles.Count);
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
            Roles = new List<UserRole>{},
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
            Roles = new List<UserRole>{},
            Email = "nicolas@example.com"
        };
        
        User user2 = new User()
        {
            Id = Guid.NewGuid(),
            FirstName = "Francisco",
            LastName = "Aguilar",
            Username = "FAguilar",
            Password = "123456",
            Roles = new List<UserRole>{},
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
            Roles = new List<UserRoleBasicInfoDTO>{},
            Email = "nicolas@example.com"
        };
        
        UserRoleBasicInfoDTO role = new UserRoleBasicInfoDTO()
        {
            Role = Role.Blogger,
        };
        
        userDTO.Roles.Add(role);
        
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

        var mock = new Mock<IUserLogic>(MockBehavior.Strict);

        var controller = new UsersController(mock.Object);
        mock.Setup(o => o.CreateUser(It.IsAny<User>())).Returns(user);
        var result = controller.CreateUser(userDTO);
        var okResult = result as CreatedResult;
        var dto = okResult.Value as UserDetailDTO;
        mock.VerifyAll();
        Assert.AreEqual(user.FirstName, dto.FirstName);
    }
    
    [TestMethod]
    public void CreateInvalidUser()
    {
        CreateUserDTO userDTO = new CreateUserDTO()
        {
            FirstName = "",
            LastName = "Hernandez",
            Username = "NicolasAHF",
            Password = "123456",
            Roles = new List<UserRoleBasicInfoDTO>{},
            Email = "nicolas@example.com"
        };
        
        UserRoleBasicInfoDTO role = new UserRoleBasicInfoDTO()
        {
            Role = Role.Blogger,
        };
        
        userDTO.Roles.Add(role);
        

        var mock = new Mock<IUserLogic>(MockBehavior.Strict);

        var controller = new UsersController(mock.Object);
        mock.Setup(o => o.CreateUser(It.IsAny<User>())).Throws(new ArgumentException());;
        var result = controller.CreateUser(userDTO);
        mock.VerifyAll();
        Assert.IsInstanceOfType(result, typeof(BadRequestObjectResult));
    }
    
    [TestMethod]
    public void UpdateValidUser()
    {
        CreateUserDTO userDTO = new CreateUserDTO()
        {
            FirstName = "Nicolas",
            LastName = "Hernandez",
            Username = "NicolasAHF",
            Password = "123456",
            Roles = new List<UserRoleBasicInfoDTO>{},
            Email = "nicolas@example.com"
        };
        
        UserRoleBasicInfoDTO role = new UserRoleBasicInfoDTO()
        {
            Role = Role.Blogger,
        };
        
        userDTO.Roles.Add(role);
        
        User user = new User()
        {
            Id = Guid.NewGuid(),
            FirstName = "Nicolas",
            LastName = "Fusco",
            Username = "NicolasAHF",
            Password = "123456",
            Roles = new List<UserRole>{},
            Email = "nicolas@example.com"
        };

        var mock = new Mock<IUserLogic>(MockBehavior.Strict);

        var controller = new UsersController(mock.Object);
        mock.Setup(o => o.UpdateUser(user.Id, It.IsAny<User>())).Returns(user);
        var result = controller.UpdateUser(user.Id, userDTO);
        var okResult = result as CreatedResult;
        var dto = okResult.Value as UserDetailDTO;
        mock.VerifyAll();
        Assert.AreNotEqual(userDTO.LastName, dto.LastName);
    }
    
    [TestMethod]
    public void UpdateInvalidUser()
    {
        var id = new Guid("f2929c98-e6f8-4d48-8d36-9eccb6fe7558");
        
        CreateUserDTO userDTO = new CreateUserDTO()
        {
            FirstName = "",
            LastName = "Hernandez",
            Username = "NicolasAHF",
            Password = "123456",
            Roles = new List<UserRoleBasicInfoDTO>{},
            Email = "nicolas@example.com"
        };
        
        UserRoleBasicInfoDTO role = new UserRoleBasicInfoDTO()
        {
            Role = Role.Blogger,
        };
        
        userDTO.Roles.Add(role);
        

        var mock = new Mock<IUserLogic>(MockBehavior.Strict);

        var controller = new UsersController(mock.Object);
        mock.Setup(o => o.UpdateUser(id, It.IsAny<User>())).Throws(new ArgumentException());
        var result = controller.UpdateUser(id, userDTO);
        mock.VerifyAll();
        Assert.IsInstanceOfType(result, typeof(BadRequestObjectResult));
    }
    
    [TestMethod]
    public void DeleteValidUser()
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

        var mock = new Mock<IUserLogic>(MockBehavior.Strict);

        var controller = new UsersController(mock.Object);
        mock.Setup(o => o.DeleteUser(user.Id));
        var result = controller.DeleteUser(user.Id);
        var okResult = result as OkObjectResult;
        mock.VerifyAll();
        Assert.AreEqual(okResult.Value, $"User with the id {user.Id} was deleted");
    }
    
    [TestMethod]
    public void DeleteInvalidUser()
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

        var mock = new Mock<IUserLogic>(MockBehavior.Strict);

        var controller = new UsersController(mock.Object);
        mock.Setup(o => o.DeleteUser(user.Id)).Throws(new NotFoundException("There are no users."));
        var result = controller.DeleteUser(user.Id);
        mock.VerifyAll();
        Assert.IsInstanceOfType(result, typeof(NotFoundObjectResult));
    }
    
    
}