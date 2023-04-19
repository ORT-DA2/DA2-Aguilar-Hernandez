using Blog.Domain.Entities;

namespace Blog.IBusinessLogic;

public interface ISessionService
{
    User? GetLoggedUser(Guid token);
    Guid Login(string email, string password);
    void Logout();
}