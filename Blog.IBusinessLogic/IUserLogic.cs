using Blog.Domain.Entities;

namespace Blog.IBusinessLogic;

public interface IUserLogic
{ 
        public User GetUserById(Guid id);
        public IEnumerable<User> GetAllUsers();
        public User CreateUser(User user);
        public User UpdateUser(Guid id, User user, Guid auth);
        public void DeleteUser(Guid id);

        void UserAlreadyExist(User userExist);
        void ValidateNull(User user);
        void GeneralValidation(User user);
}