using Blog.Domain.Entities;

namespace Blog.DataAccess;

public class UserRepository: IUserRepository
{
    private BlogDbContext _blogDbContext;

    public UserRepository(BlogDbContext blogDbContext)
    {
        _blogDbContext = blogDbContext;
    }
    public User CreateUser(User user)
    {
        return user;
    }
}