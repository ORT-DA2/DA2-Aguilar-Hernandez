using Blog.Domain.Entities;

namespace Blog.IBusinessLogic;

public interface ISessionLogic
{
    User? GetLoggedUser(Guid token);
    Guid Login(string email, string password);
    void Logout(Guid token);
}