﻿using System.Linq.Expressions;
using Blog.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Blog.DataAccess;

public class CommentRepository: Repository<Comment>
{
    public CommentRepository(BlogDbContext context) : base(context)
    {
    }
    
    public override Comment? GetById(Expression<Func<Comment, bool>> expression)
    {
        return _context.Set<Comment>().Include(c => c.Article).Include(c=>c.Owner).FirstOrDefault(expression);
    }
}