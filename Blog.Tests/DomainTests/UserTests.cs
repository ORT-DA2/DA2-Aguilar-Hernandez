using Blog.Domain.Entities;
using Blog.Domain.Enums;

namespace Blog.Tests.DomainTests;

[TestClass]
public class UserTests
{
    [TestMethod]
    public void GetAndSetUserTest()
    {
        User user = new User();
        user.Id = new Guid();
        Guid id = user.Id;
        user.FirstName = "Nicolas";
        user.LastName = "Hernandez";
        user.Username = "NicolasAHF";
        user.Email = "nicolashernandez@example.com";
        user.Role = Role.Blogger;

        Assert.AreEqual(id, user.Id);
        Assert.AreEqual("Nicolas", user.FirstName);
        Assert.AreEqual("Hernandez", user.LastName);
        Assert.AreEqual("NicolasAHF", user.Username);
        Assert.AreEqual("nicolashernandez@example.com", user.Email);
        Assert.AreEqual(Role.Blogger, user.Role);
    }

    [TestMethod]
    public void StringValidationSuccessTest()
    {

        User user = new User()
        {
            Id = Guid.NewGuid(),
            FirstName = "Francisco",
            LastName = "Aguilar",
            Username = "FAguilar",
            Password = "123456",
            Role = Role.Admin,
            Email = "Francisco@example.com"
        };
        user.ValidateEmptyString();
    }
    
    [TestMethod]
    [ExpectedException(typeof(ArgumentException),
        "Empty FirstName")]
    public void StringValidationFailTest()
    {
        User user = new User()
        {
            Id = Guid.NewGuid(),
            FirstName = "",
            LastName = "Aguilar",
            Username = "FAguilar",
            Password = "123456",
            Role = Role.Admin,
            Email = "Francisco@example.com"
        };
        user.ValidateEmptyString();
    }
    
    [TestMethod]
    public void AlfanumericValidationSuccessTest()
    {
        User user = new User()
        {
            Id = Guid.NewGuid(),
            FirstName = "Francisco",
            LastName = "Aguilar",
            Username = "FAguilar",
            Password = "123456",
            Role = Role.Admin,
            Email = "Francisco@example.com"
        };
        user.ValidateAlfanumericUsername();
        
    }
    
    [TestMethod]
    [ExpectedException(typeof(ArgumentException),
        "Username should be alphanumeric")]
    public void AlfanumericValidationFailTest()
    {
        User user = new User()
        {
            Id = Guid.NewGuid(),
            FirstName = "Francisco",
            LastName = "Aguilar",
            Username = "@Nicolas.AHF",
            Password = "123456",
            Role = Role.Admin,
            Email = "Francisco@example.com"
        };
        user.ValidateAlfanumericUsername();
    }
    
    [TestMethod]
    public void EmailValidationSuccessTest()
    {
        User user = new User()
        {
            Id = Guid.NewGuid(),
            FirstName = "Francisco",
            LastName = "Aguilar",
            Username = "FAguilar",
            Password = "123456",
            Role = Role.Admin,
            Email = "Francisco@example.com"
        };
        user.ValidateEmail();
    }
    
    [TestMethod]
    [ExpectedException(typeof(ArgumentException),
        "Invalid Email")]
    public void EmailValidationFailTest()
    {
        User user = new User()
        {
            Id = Guid.NewGuid(),
            FirstName = "Francisco",
            LastName = "Aguilar",
            Username = "FAguilar",
            Password = "123456",
            Role = Role.Admin,
            Email = "nicolascom"
        };
        user.ValidateEmail();

    }
}
