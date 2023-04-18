using System.Text.Json;
using System.Text.Json.Serialization;
using Blog.BusinessLogic.Exceptions;
using Blog.IDataAccess;
using Blog.Domain.Entities;
using Blog.IBusinessLogic;


namespace Blog.BusinessLogic;

public class UserLogic: IUserLogic
{
    private readonly IRepository<User> _repository;

    public UserLogic(IRepository<User> userRepository)
    {
        _repository = userRepository;
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
        user.ValidateEmptyString();
        user.ValidateEmail();
        user.ValidateAlfanumericUsername();
        user.ValidateUsernameLenght();
        _repository.Insert(user);
        _repository.Save();
        return user;
    }

    public User UpdateUser(Guid id, User userUpdated)
    {
        userUpdated.ValidateEmptyString();
        userUpdated.ValidateEmail();
        userUpdated.ValidateAlfanumericUsername();
        userUpdated.ValidateUsernameLenght();
        
        var oldUser = _repository.GetById(u => u.Id == id);

        if (oldUser == null)
        {
            throw new NotFoundException($"The user was not found");
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
            throw new NotFoundException($"The user was not found");
        }
        
        _repository.Delete(user);
        _repository.Save();
    }
}