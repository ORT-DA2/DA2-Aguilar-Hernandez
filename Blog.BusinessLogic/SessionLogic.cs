using System.Security.Authentication;
using Blog.Domain.Entities;
using Blog.IBusinessLogic;
using Blog.IDataAccess;

namespace Blog.BusinessLogic;

public class SessionLogic: ISessionLogic
{
    private IRepository<Session> _sessionRepository;
    private IRepository<User> _userRepository;
    private IUserLogic _userLogic;

    public SessionLogic(IRepository<Session> sessionRepository, IRepository<User> userRepository, IUserLogic userLogic)
    {
        _sessionRepository = sessionRepository;
        _userRepository = userRepository;
        _userLogic = userLogic;
    }

    public User? GetLoggedUser(Guid token)
    {
        Session session = _sessionRepository.GetBy(s => s.AuthToken == token);
        if (session != null)
            return session.User;
        else
        {
            throw new KeyNotFoundException("User not found");
        }
    }

    public Guid Login(string username, string password)
    {
        User user = new User();
        user = _userRepository.GetBy(u => u.Username.Equals(username) && u.Password.Equals(password));
        if (user == null)
        {
            throw new InvalidCredentialException("Invalid credentials");
        }
        

        Session session = new Session() { User = user };
        _sessionRepository.Insert(session);
        _sessionRepository.Save();
        return session.AuthToken;
    }

    public void Logout(Guid token)
    {
        Session session = _sessionRepository.GetBy(s => s.AuthToken == token);
        _sessionRepository.Delete(session);
        _sessionRepository.Save();
    }

    public User Register(User user)
    {
        var userExist = _userRepository.GetBy(u => u.Username == user.Username);
        _userLogic.UserAlreadyExist(userExist);
        _userLogic.ValidateNull(user);
        _userLogic.GeneralValidation(user);
        _userRepository.Insert(user);
        _userRepository.Save();
        return user;
    }
}