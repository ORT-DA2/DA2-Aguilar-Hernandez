using Blog.Domain.Entities;

namespace Blog.IBusinessLogic;

public interface IUserLogic
{ 
        public User GetUserById(Guid id);
        public IEnumerable<User> GetAllUsers();
        public User CreateUser(User user);
        public User UpdateUser(Guid id, User user, Guid auth);
        public void DeleteUser(Guid id);
        public Dictionary<string, int> UserActivityRanking(DateTime startDate, DateTime endDate);
        public Dictionary<string, int> UserOffensiveRanking(DateTime startDate, DateTime endDate);
}