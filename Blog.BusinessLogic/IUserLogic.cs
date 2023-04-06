using Blog.Domain.Entities;

namespace Blog.BusinessLogic;

public interface IUserLogic
{
    public User GetUserById(Guid id);
    public List<User> GetAllUsers();
}