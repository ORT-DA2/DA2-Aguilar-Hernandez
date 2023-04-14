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
        user.Roles = new List<UserRole>{};
        
        UserRole role = new UserRole()
        {
            Role = Role.Blogger,
            UserId = user.Id,
            User = user
        };
        
        user.Roles.Add(role);

        Assert.AreEqual(id, user.Id);
        Assert.AreEqual("Nicolas", user.FirstName);
        Assert.AreEqual("Hernandez", user.LastName);
        Assert.AreEqual("NicolasAHF", user.Username);
        Assert.AreEqual("nicolashernandez@example.com", user.Email);
        Assert.AreEqual(new List<UserRole>{role}.Count, user.Roles.Count);
    }

    [TestMethod]
    public void StringEmptyValidationSuccessTest()
    {

        User user = new User()
        {
            Id = Guid.NewGuid(),
            FirstName = "Francisco",
            LastName = "Aguilar",
            Username = "FAguilar",
            Password = "123456",
            Roles = new List<UserRole>{},
            Email = "Francisco@example.com"
        };
        
        UserRole role = new UserRole()
        {
            Role = Role.Blogger,
            UserId = user.Id,
            User = user
        };
        
        user.Roles.Add(role);
        
        user.ValidateEmptyString();
    }
    
    [TestMethod]
    [ExpectedException(typeof(ArgumentException),
        "Empty FirstName")]
    public void FirstNameEmptyValidationFailTest()
    {
        User user = new User()
        {
            Id = Guid.NewGuid(),
            FirstName = "",
            LastName = "Aguilar",
            Username = "FAguilar",
            Password = "123456",
            Roles = new List<UserRole>{},
            Email = "Francisco@example.com"
        };
        
        UserRole role = new UserRole()
        {
            Role = Role.Blogger,
            UserId = user.Id,
            User = user
        };
        
        user.Roles.Add(role);
        
        user.ValidateEmptyString();
    }
    
    [TestMethod]
    [ExpectedException(typeof(ArgumentException),
        "Empty LastName")]
    public void LastNameEmptyValidationFailTest()
    {
        User user = new User()
        {
            Id = Guid.NewGuid(),
            FirstName = "Francisco",
            LastName = "",
            Username = "FAguilar",
            Password = "123456",
            Roles = new List<UserRole>{},
            Email = "Francisco@example.com"
        };
        
        UserRole role = new UserRole()
        {
            Role = Role.Blogger,
            UserId = user.Id,
            User = user
        };
        
        user.Roles.Add(role);
        
        user.ValidateEmptyString();
    }
    
    [TestMethod]
    [ExpectedException(typeof(ArgumentException),
        "Empty Username")]
    public void UsernameEmptyValidationFailTest()
    {
        User user = new User()
        {
            Id = Guid.NewGuid(),
            FirstName = "Francisco",
            LastName = "Aguilar",
            Username = "",
            Password = "123456",
            Roles = new List<UserRole>{},
            Email = "Francisco@example.com"
        };
        
        UserRole role = new UserRole()
        {
            Role = Role.Blogger,
            UserId = user.Id,
            User = user
        };
        
        user.Roles.Add(role);
        
        user.ValidateEmptyString();
    }
    
    [TestMethod]
    [ExpectedException(typeof(ArgumentException),
        "Empty Password")]
    public void PasswordEmptyValidationFailTest()
    {
        User user = new User()
        {
            Id = Guid.NewGuid(),
            FirstName = "Francisco",
            LastName = "Aguilar",
            Username = "FAguilar",
            Password = "",
            Roles = new List<UserRole>{},
            Email = "Francisco@example.com"
        };
        
        UserRole role = new UserRole()
        {
            Role = Role.Blogger,
            UserId = user.Id,
            User = user
        };
        
        user.Roles.Add(role);
        
        user.ValidateEmptyString();
    }
    
    [TestMethod]
    [ExpectedException(typeof(ArgumentException),
        "Empty Email")]
    public void EmailEmptyValidationFailTest()
    {
        User user = new User()
        {
            Id = Guid.NewGuid(),
            FirstName = "Francisco",
            LastName = "Aguilar",
            Username = "FAguilar",
            Password = "123456",
            Roles = new List<UserRole>{},
            Email = ""
        };
        
        UserRole role = new UserRole()
        {
            Role = Role.Blogger,
            UserId = user.Id,
            User = user
        };
        
        user.Roles.Add(role);
        
        user.ValidateEmptyString();
    }
    
    [TestMethod]
    public void AlfanumericUsernameValidationSuccessTest()
    {
        User user = new User()
        {
            Id = Guid.NewGuid(),
            FirstName = "Francisco",
            LastName = "Aguilar",
            Username = "FAguilar",
            Password = "123456",
            Roles = new List<UserRole>{},
            Email = "Francisco@example.com"
        };
        
        UserRole role = new UserRole()
        {
            Role = Role.Blogger,
            UserId = user.Id,
            User = user
        };
        
        user.Roles.Add(role);
        
        user.ValidateAlfanumericUsername();
        
    }
    
    [TestMethod]
    [ExpectedException(typeof(ArgumentException),
        "Username must be between 12 and 4 characters")]
    public void UsernameLenghtValidationLessThan4FailTest()
    {
        User user = new User()
        {
            Id = Guid.NewGuid(),
            FirstName = "Francisco",
            LastName = "Aguilar",
            Username = "FAg",
            Password = "123456",
            Roles = new List<UserRole>{},
            Email = "Francisco@example.com"
        };
        
        UserRole role = new UserRole()
        {
            Role = Role.Blogger,
            UserId = user.Id,
            User = user
        };
        
        user.Roles.Add(role);
        
        user.ValidateUsernameLenght();
        
    }
    
    [TestMethod]
    [ExpectedException(typeof(ArgumentException),
        "Username must be between 12 and 4 characters")]
    public void UsernameLenghtValidationMoreThan4FailTest()
    {
        User user = new User()
        {
            Id = Guid.NewGuid(),
            FirstName = "Francisco",
            LastName = "Aguilar",
            Username = "FAguilar123456789",
            Password = "123456",
            Roles = new List<UserRole>{},
            Email = "Francisco@example.com"
        };
        
        UserRole role = new UserRole()
        {
            Role = Role.Blogger,
            UserId = user.Id,
            User = user
        };
        
        user.Roles.Add(role);
        
        user.ValidateUsernameLenght();
        
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
            Roles = new List<UserRole>{},
            Email = "Francisco@example.com"
        };
        
        UserRole role = new UserRole()
        {
            Role = Role.Blogger,
            UserId = user.Id,
            User = user
        };
        
        user.Roles.Add(role);
        
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
            Roles = new List<UserRole>{},
            Email = "Francisco@example.com"
        };
        
        UserRole role = new UserRole()
        {
            Role = Role.Blogger,
            UserId = user.Id,
            User = user
        };
        
        user.Roles.Add(role);
        
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
            Roles = new List<UserRole>{},
            Email = "nicolascom"
        };
        
        UserRole role = new UserRole()
        {
            Role = Role.Blogger,
            UserId = user.Id,
            User = user
        };
        
        user.Roles.Add(role);
        
        user.ValidateEmail();

    }
}
