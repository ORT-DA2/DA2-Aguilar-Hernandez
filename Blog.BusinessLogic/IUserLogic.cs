using Blog.Domain.Entities;

namespace Blog.BusinessLogic;

public interface IUserLogic
{
    public User GetUserById(Guid id);
    public List<User> GetAllUsers();
    public User CreateUser(User user);
    public User UpdateUser(Guid id, User user);
    public void DeleteUser(Guid id);
}