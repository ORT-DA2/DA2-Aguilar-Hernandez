using System.Linq.Expressions;
using Blog.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Blog.DataAccess;

public class SessionRepository: Repository<Session>
{
    public SessionRepository(BlogDbContext context) : base(context)
    {
    }
    
    public override Session? GetBy(Expression<Func<Session,bool>> expression)
    {
        return _context.Set<Session>().Include(a => a.User).ThenInclude(u => u.Roles).FirstOrDefault(expression);
    }
}