using Blog.IDataAccess;
using Blog.Domain.Entities;
using Blog.Domain.Enums;
using Blog.Domain.Exceptions;
using Blog.IBusinessLogic;


namespace Blog.BusinessLogic;

public class UserLogic: IUserLogic
{
    private readonly IRepository<User> _repository;
    private readonly IRepository<Article> _articleRepository;
    private readonly ISessionLogic _sessionLogic;

    public UserLogic(IRepository<User> userRepository, ISessionLogic sessionLogic, IRepository<Article> articleRepository)
    {
        _repository = userRepository;
        _sessionLogic = sessionLogic;
        _articleRepository = articleRepository;
    }
    public User GetUserById(Guid id)
    {
        var user = _repository.GetBy(u => u.Id == id);
        
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
        var userExist = _repository.GetBy(u => u.Username == user.Username);
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
        
        var oldUser = _repository.GetBy(u => u.Id == id);

        ValidateNull(oldUser);
        var userExistUsername = _repository.GetBy(u => u.Username == userUpdated.Username);
        var userExistEmail = _repository.GetBy(u => u.Email == userUpdated.Email);
        UsernameAlreadyExistUpdate(userExistUsername, oldUser);
        EmailAlreadyExistUpdate(userExistEmail, oldUser);

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
        var user = _repository.GetBy(u => u.Id == id);

        ValidateNull(user);
        
        _repository.Delete(user);
        _repository.Save();
    }

    public Dictionary<string, int> UserActivityRanking(DateTime startDate, DateTime endDate)
    {
        ValidateDates(startDate, endDate);
        IEnumerable<User> users = _repository.GetAll();
        IEnumerable<Article> articles = _articleRepository.GetAll().Where(a => a.DatePublished >= startDate && a.DatePublished <= endDate.AddDays(1).Date.AddSeconds(-1));
        Dictionary<string, int> articleCounts = articles
            .GroupBy(a => a.Owner.Username)
            .ToDictionary(g => g.Key, g => g.Count());
        Dictionary<string, int> commentCounts = users.ToDictionary(user => user.Username, user => user.Comments.Where(c => c.DatePublished >= startDate && c.DatePublished <= endDate.AddDays(1).Date.AddSeconds(-1)).Count());
        Dictionary<string, int> counts = articleCounts
            .Concat(commentCounts)
            .GroupBy(d => d.Key)
            .ToDictionary(g => g.Key, g => g.Sum(d => d.Value));
        return counts;
    }

    private void ValidateDates(DateTime startDate, DateTime endDate)
    {
        if (startDate > endDate)
        {
            throw new ArgumentException("Start date couldn't be set after end Date");
        }
    }

    public void GeneralValidation(User user)
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

    public void UserAlreadyExist(User user)
    {
        if (user != null)
        {
            throw new ArgumentException("User already exists");
        }
    }
    
    public static void UsernameAlreadyExistUpdate(User user, User oldUser)
    {
        if (user != null && user.Id != oldUser.Id)
        {
            throw new ArgumentException("User with that username already exists");
        }
    }
    
    public static void EmailAlreadyExistUpdate(User user, User oldUser)
    {
        if (user != null && user.Id != oldUser.Id)
        {
            throw new ArgumentException("User with that email already exists");
        }
    }

    public void ValidateNull(User user)
    {
        if (user == null)
        {
            throw new NotFoundException("The user was not found");
        }
    }
    
    public void ValidateListNull(IEnumerable<User> users)
    {
        if (users == null || !users.Any())
        {
            throw new NotFoundException("The are no users");
        }
    }
}