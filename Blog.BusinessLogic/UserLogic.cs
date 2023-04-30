using Blog.IDataAccess;
using Blog.Domain.Entities;
using Blog.Domain.Enums;
using Blog.Domain.Exceptions;
using Blog.IBusinessLogic;


namespace Blog.BusinessLogic;

public class UserLogic: IUserLogic
{
    private readonly IRepository<User> _repository;
    private readonly ISessionLogic _sessionLogic;

    public UserLogic(IRepository<User> userRepository, ISessionLogic sessionLogic)
    {
        _repository = userRepository;
        _sessionLogic = sessionLogic;
    }
    public User GetUserById(Guid id)
    {
        var user = _repository.GetById(u => u.Id == id);
        
        ValidateNull(user);
        
        return user;
    }

    public IEnumerable<User> GetAllUsers()
    {
        var users = _repository.GetAll();
        ValidateListNull(users);
        
        return users;
    }

    public User CreateUser(User user)
    {
        var userExist = _repository.GetByUsername(u => u.Username == user.Username);
        UserAlreadyExist(userExist);
        ValidateNull(user);
        GeneralValidation(user);
        _repository.Insert(user);
        _repository.Save();
        return user;
    }

    public User UpdateUser(Guid id, User userUpdated, Guid auth)
    {
        GeneralValidation(userUpdated);
        
        var oldUser = _repository.GetById(u => u.Id == id);

        ValidateNull(oldUser);
        var userExist = _repository.GetByUsername(u => u.Username == userUpdated.Username);
        UsernameAlreadyExistUpdate(userExist, oldUser);

        if (_sessionLogic.GetLoggedUser(auth).Roles.All(ur => ur.Role != Role.Admin ))
        {
            if (_sessionLogic.GetLoggedUser(auth).Id != id)
            {
                throw new ArgumentException("You can´t update other user");
            }
        }

        oldUser.UpdateAttributes(userUpdated);
        _repository.Update(oldUser);
        _repository.Save();

        return oldUser;
    }

    public void DeleteUser(Guid id)
    {
        var user = _repository.GetById(u => u.Id == id);

        ValidateNull(user);
        
        _repository.Delete(user);
        _repository.Save();
    }

    private static void GeneralValidation(User user)
    {
        user.FirstNameValidation();
        user.UsernameValidation();
        user.LastNameValidation();
        user.PasswordValidation();
        user.EmailValidation();
        user.ValidateEmail();
        user.ValidateAlfanumericUsername();
        user.ValidateUsernameLenght();
        user.ValidatePasswordLenght();
    }

    private static void UserAlreadyExist(User user)
    {
        if (user != null)
        {
            throw new ArgumentException("User already exists");
        }
    }
    
    private static void UsernameAlreadyExistUpdate(User user, User oldUser)
    {
        if (user != null && user.Id != oldUser.Id)
        {
            throw new ArgumentException("User with that username already exists");
        }
    }

    private static void ValidateNull(User user)
    {
        if (user == null)
        {
            throw new NotFoundException("The user was not found");
        }
    }
    
    private static void ValidateListNull(IEnumerable<User> users)
    {
        if (users == null || !users.Any())
        {
            throw new NotFoundException("The are no users");
        }
    }
}