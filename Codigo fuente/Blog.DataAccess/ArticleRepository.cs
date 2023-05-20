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
        return _context.Set<Article>()
            .Include(u => u.Owner).ThenInclude(ur => ur.Roles)
            .Include(c => c.Comments)
            .Include(a => a.OffensiveContent)
            .ToList();
    }
    
    public override Article? GetBy(Expression<Func<Article, bool>> expression)
    {
        return _context.Set<Article>()
            .Include(u => u.Owner).ThenInclude(ur => ur.Roles)
            .Include(c => c.Comments)
            .Include(a => a.OffensiveContent)
            .FirstOrDefault(expression);
    }
    public override  IEnumerable<Article> GetByText(string text)
    {
        return _context.Set<Article>()
            .Include(u => u.Owner).ThenInclude(ur => ur.Roles)
            .Include(c => c.Comments).ThenInclude(c => c.Owner)
            .Include(a => a.OffensiveContent)
            .Where(a => (a.Title.Contains(text) || a.Content.Contains(text)) && a.IsPublic == true)
            .ToList();
    }
    
    public override IEnumerable<Article> GetLastTen()
    {
        return _context.Set<Article>()
            .Include(u => u.Owner).ThenInclude(ur => ur.Roles)
            .Include(c => c.Comments)
            .Include(a => a.OffensiveContent)
            .OrderByDescending(a => a.DatePublished)
            .Where(a => a.IsPublic == true).Take(10).ToList();
    }
    
    public override IEnumerable<Article> GetPublicAll()
    {
        return _context.Set<Article>()
            .Include(u => u.Owner).ThenInclude(ur => ur.Roles)
            .Include(c => c.Comments)
            .Include(a => a.OffensiveContent)
            .Where(a => a.IsPublic == true).ToList();
    }
    
    public override IEnumerable<Article> GetUserArticles(string username)
    {
        return _context.Set<Article>()
            .Include(u => u.Owner).ThenInclude(ur => ur.Roles)
            .Include(c => c.Comments)
            .Include(a => a.OffensiveContent)
            .Where(a => a.Owner.Username == username).ToList();
    }
    
}