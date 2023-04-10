using Blog.Domain.Entities;

namespace Blog.DataAccess;

public interface IUserRepository
{
    public User CreateUser(User user);
}