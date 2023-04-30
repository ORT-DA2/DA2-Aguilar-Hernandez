using System.Linq.Expressions;
using Blog.Domain.Entities;
using Blog.IDataAccess;
using Microsoft.EntityFrameworkCore;

namespace Blog.DataAccess;

public class Repository<T> : IRepository<T> where T : class
{
    protected readonly BlogDbContext _context;

    public Repository(BlogDbContext context)
    {
        _context = context;
    }
    
    public virtual IEnumerable<T> GetAll()
    {
        return _context.Set<T>();
    }

    public virtual T? GetById(Expression<Func<T, bool>> expression)
    {
        return _context.Set<T>().FirstOrDefault(expression);
    }

    public void Insert(T elem)
    {
        _context.Set<T>().Add(elem);
    }

    public void Delete(T elem)
    {
        _context.Set<T>().Remove(elem);
    }

    public void Update(T elem)
    {
        _context.Set<T>().Update(elem);
    }

    public void Save()
    {
        _context.SaveChanges();
    }

    public virtual IEnumerable<T> GetByText(string text)
    {
        return _context.Set<T>();
    }

    public virtual IEnumerable<T> GetLastTen()
    {
        return _context.Set<T>();
    }

    public virtual IEnumerable<T> GetPublicAll()
    {
        return _context.Set<T>();
    }

    public virtual IEnumerable<T> GetPrivateAll(string username)
    {
        return _context.Set<T>();
    }

    public T? GetByUsername(Expression<Func<T, bool>> expression)
    {
        return _context.Set<T>().FirstOrDefault(expression);
    }
    
}