﻿using System.Linq.Expressions;
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
    
    public override Article? GetBy(Expression<Func<Article, bool>> expression)
    {
        return _context.Set<Article>().Include(u => u.Owner).ThenInclude(ur => ur.Roles).Include(c => c.Comments).FirstOrDefault(expression);
    }
    public override  IEnumerable<Article> GetByText(string text)
    {
        return _context.Set<Article>().Include(u => u.Owner).ThenInclude(ur => ur.Roles).Include(c => c.Comments).Where(a => a.Title.Contains(text) || a.Content.Contains(text) && a.IsPublic == true);
    }
    
    public override IEnumerable<Article> GetLastTen()
    {
        return _context.Set<Article>().Include(u => u.Owner).ThenInclude(ur => ur.Roles).Include(c => c.Comments).OrderByDescending(a => a.DatePublished).Where(a => a.IsPublic == true).Take(10);
    }
    
    public override IEnumerable<Article> GetPublicAll()
    {
        return _context.Set<Article>().Include(u => u.Owner).ThenInclude(ur => ur.Roles).Include(c => c.Comments).Where(a => a.IsPublic == true);
    }
    
    public override IEnumerable<Article> GetUserArticles(string username)
    {
        return _context.Set<Article>().Include(u => u.Owner).ThenInclude(ur => ur.Roles).Include(c => c.Comments).Where(a => a.Owner.Username == username);
    }
    
}