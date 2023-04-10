using Blog.BusinessLogic.Exceptions;
using Blog.DataAccess;
using Blog.Domain.Entities;

namespace Blog.BusinessLogic;

public class UserLogic: IUserLogic
{
    private readonly IUserRepository _userRepository;

    public UserLogic(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }
    public User GetUserById(Guid id)
    {
        throw new NotImplementedException();
    }

    public List<User> GetAllUsers()
    {
        throw new NotImplementedException();
    }

    public User CreateUser(User user)
    {
        return _userRepository.CreateUser(user);
    }
}