using Blog.Domain.Entities;

namespace Blog.IDataAccess;

public interface IUserRepository
{
    public User CreateUser(User user);
}