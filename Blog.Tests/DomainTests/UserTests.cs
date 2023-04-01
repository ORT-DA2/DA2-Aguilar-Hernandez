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
}
