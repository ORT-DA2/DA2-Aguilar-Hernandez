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
        User user = new User();
        user.FirstName = "Nicolas";
        var result = user.ValidateEmptyString(user.FirstName);
        
        Assert.IsFalse(result);
    }
    
    [TestMethod]
    public void StringValidationFailTest()
    {
        User user = new User()
        {
            FirstName = ""
        };
        var result = user.ValidateEmptyString(user.FirstName);
        
        Assert.IsTrue(result);
    }
    
    [TestMethod]
    public void AlfanumericValidationSuccessTest()
    {
        User user = new User();
        user.Username = "NicolasAHF";
        var result = user.ValidateAlfanumeric(user.Username);
        
        Assert.IsTrue(result);
    }
    
    [TestMethod]
    public void AlfanumericValidationFailTest()
    {
        User user = new User()
        {
            Username = "@Nicolas.AHF"
        };
        var result = user.ValidateAlfanumeric(user.Username);
        
        Assert.IsFalse(result);
    }
    
    [TestMethod]
    public void EmailValidationSuccessTest()
    {
        User user = new User()
        {
            Email = "nicolas@example.com"
        };
        var result = user.ValidateEmail(user.Email);
        
        Assert.IsTrue(result);
    }
    
    [TestMethod]
    public void EmailValidationFailTest()
    {
        User user = new User()
        {
            Email = "nicolas.com"
        };
        var result = user.ValidateEmail(user.Email);
        
        Assert.IsFalse(result);
    }
}
