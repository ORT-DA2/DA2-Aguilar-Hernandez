using System.Linq.Expressions;
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
    
    public IEnumerable<T> GetAll()
    {
        return _context.Set<T>();
    }

    public T? GetById(Expression<Func<T, bool>> expression)
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
        _context.Entry(elem).State = EntityState.Modified;
        _context.Set<T>().Update(elem);
    }

    public void Save()
    {
        _context.SaveChanges();
    }
}