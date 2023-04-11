using Blog.IDataAccess;
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
        user.ValidateEmptyString();
        user.ValidateEmail();
        user.ValidateAlfanumericUsername();
        user.ValidateUsernameLenght();
        return _userRepository.CreateUser(user);
    }

    public User UpdateUser(User user)
    {
        throw new NotImplementedException();
    }

    public void DeleteUser(Guid id)
    {
        throw new NotImplementedException();
    }
}