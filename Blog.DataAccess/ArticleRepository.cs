using System.Linq.Expressions;
using Blog.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Blog.DataAccess;

public class ArticleRepository: Repository<Article>
{
    public ArticleRepository(BlogDbContext context) : base(context)
    {
    }
    
    public override IEnumerable<Article> GetAll()
    {
        return _context.Set<Article>().Include(u => u.Owner).ThenInclude(ur => ur.Roles).Include(c => c.Comments);
    }
    
    public override Article? GetById(Expression<Func<Article, bool>> expression)
    {
        return _context.Set<Article>().Include(u => u.Owner).ThenInclude(ur => ur.Roles).Include(c => c.Comments).FirstOrDefault(expression);
    }
    public override  IEnumerable<Article> GetByText(string text)
    {
        return _context.Set<Article>().Include(u => u.Owner).ThenInclude(ur => ur.Roles).Include(c => c.Comments).Where(a => a.Title.Contains(text) || a.Content.Contains(text));
    }
    
    public override IEnumerable<Article> GetLastTen()
    {
        return _context.Set<Article>().Include(u => u.Owner).ThenInclude(ur => ur.Roles).Include(c => c.Comments).OrderByDescending(a => a.DatePublished).Take(10).Where(a => a.IsPublic == true);
    }
}