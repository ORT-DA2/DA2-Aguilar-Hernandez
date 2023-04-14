using Blog.IDataAccess;
using Blog.Domain.Entities;


namespace Blog.BusinessLogic;

public class UserLogic: IUserLogic
{
    private readonly IRepository<User> _userRepository;

    public UserLogic(IRepository<User> userRepository)
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
        _userRepository.Insert(user);
        return user;
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