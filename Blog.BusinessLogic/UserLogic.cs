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
        return _repository.GetById(u => u.Id == id);
    }

    public IEnumerable<User> GetAllUsers()
    {
        return _repository.GetAll();
    }

    public User CreateUser(User user)
    {
        GeneralValidation(user);
        _repository.Insert(user);
        _repository.Save();
        return user;
    }

    public User UpdateUser(Guid id, User userUpdated, Guid auth)
    {
        GeneralValidation(userUpdated);
        
        var oldUser = _repository.GetById(u => u.Id == id);

        if (oldUser == null)
        {
            throw new NotFoundException("The user was not found");
        }

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
        
        if (user == null)
        {
            throw new NotFoundException("The user was not found");
        }
        
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
}