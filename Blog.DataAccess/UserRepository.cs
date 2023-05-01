using System.Linq.Expressions;
using Blog.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Blog.DataAccess;

public class UserRepository: Repository<User>
{
    public UserRepository(BlogDbContext context) : base(context)
    {
    }

    public override IEnumerable<User> GetAll()
    {
        return _context.Set<User>().Include(u => u.Roles);
    }
    
    public override User? GetBy(Expression<Func<User, bool>> expression)
    {
        return _context.Set<User>().Include(u => u.Roles).FirstOrDefault(expression);
    }
    
}