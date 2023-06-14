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
        User? user = _repository.GetBy(u => u.Id == id);
        
        ValidateNull(user);
        
        return user;
    }
    
    public User GetUserByUsername(string username)
    {
        User? user = _repository.GetBy(u => u.Username == username);
        
        ValidateNull(user);
        
        return user;
    }

    public IEnumerable<User> GetAllUsers()
    {
        IEnumerable<User> users = _repository.GetAll();
        ValidateListNull(users);
        
        return users;
    }

    public User CreateUser(User user)
    {
        User? userExist = _repository.GetBy(u => u.Username == user.Username);
        UserAlreadyExist(userExist);
        ValidateNull(user);
        GeneralValidation(user, false);
        _repository.Insert(user);
        _repository.Save();
        return user;
    }

    public User UpdateUser(Guid id, User userUpdated, Guid auth)
    {
        GeneralValidation(userUpdated, true);
        
        User? oldUser = _repository.GetBy(u => u.Id == id);

        ValidateNull(oldUser);
        User? userExistUsername = _repository.GetBy(u => u.Username == userUpdated.Username);
        User? userExistEmail = _repository.GetBy(u => u.Email == userUpdated.Email);
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
        User? user = _repository.GetBy(u => u.Id == id);

        ValidateNull(user);
        
        _repository.Delete(user);
        _repository.Save();
    }

    public Dictionary<string, int> UserActivityRanking(DateTime startDate, DateTime endDate)
    {
        ValidateDates(startDate, endDate);
        IEnumerable<User> users = _repository.GetAll();
        IEnumerable<Article> articles = _articleRepository.GetAll().Where(a => a.DatePublished >= startDate && a.DatePublished <= endDate.AddDays(1).Date.AddSeconds(-1));
        Dictionary<string, int> articleCounts = ArticlesPerUser(articles);
        Dictionary<string, int> commentCounts = CommentsPerUser(startDate, endDate, users);
        Dictionary<string, int> counts = ActivityPerUser(articleCounts, commentCounts);
        return counts;
    }

    public Dictionary<string, int> UserOffensiveRanking(DateTime startDate, DateTime endDate)
    {
        ValidateDates(startDate, endDate);
        IEnumerable<User> users = _repository.GetAll();
        IEnumerable<Article> articles = _articleRepository.GetAll().Where(a => a.DatePublished >= startDate && a.DatePublished <= endDate.AddDays(1).Date.AddSeconds(-1) && (a.IsEdited || a.OffensiveContent.Any()));
        Dictionary<string, int> articleCounts = ArticlesPerUser(articles);
        Dictionary<string, int> commentCounts = CommentsPerUser(startDate, endDate, users);
        Dictionary<string, int> counts = ActivityPerUser(articleCounts, commentCounts);
        return counts;
    }
    
    private Dictionary<string, int> ArticlesPerUser(IEnumerable<Article> articles)
    {
        return articles
            .GroupBy(a => a.Owner.Username)
            .ToDictionary(g => g.Key, g => g.Count());
    }

    private Dictionary<string, int> CommentsPerUser(DateTime startDate, DateTime endDate, IEnumerable<User> users)
    {
        return users.ToDictionary(user => user.Username, user => user.Comments.Where(c => c.DatePublished >= startDate && c.DatePublished <= endDate.AddDays(1).Date.AddSeconds(-1)).Count());
    }

    private Dictionary<string, int> ActivityPerUser(Dictionary<string, int> articlesPerUser,
        Dictionary<string, int> commentsPerUser)
    {
        return articlesPerUser
            .Concat(commentsPerUser)
            .GroupBy(d => d.Key)
            .ToDictionary(g => g.Key, g => g.Sum(d => d.Value));
    }

    private void ValidateDates(DateTime startDate, DateTime endDate)
    {
        if (startDate > endDate)
        {
            throw new ArgumentException("Start date couldn't be set after end Date");
        }
    }

    public void GeneralValidation(User user, bool update)
    {
        user.FirstNameValidation();
        user.UsernameValidation();
        user.LastNameValidation();
        user.EmailValidation();
        user.ValidateEmail();
        user.ValidateAlfanumericUsername();
        user.ValidateUsernameLenght();
        if (!update)
        {
            user.ValidatePasswordLenght();
            user.PasswordValidation();
        }
        
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