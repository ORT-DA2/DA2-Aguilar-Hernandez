using System.Linq.Expressions;
using Blog.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Blog.DataAccess;

public class CommentRepository: Repository<Comment>
{
    public CommentRepository(BlogDbContext context) : base(context)
    {
    }
    
    public override IEnumerable<Comment> GetAll()
    {
        return _context.Set<Comment>().Include(c => c.Article).Include(c=>c.Owner).Include(c => c.OffensiveContent);
    }
    
    public override Comment? GetBy(Expression<Func<Comment, bool>> expression)
    {
        return _context.Set<Comment>().Include(c => c.Article).Include(c=>c.Owner).FirstOrDefault(expression);
    }
    
}