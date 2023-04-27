using System.Linq.Expressions;
using Blog.Domain.Entities;

namespace Blog.IDataAccess;

public interface IRepository<T> where T : class
{
    IEnumerable<T> GetAll();
    T? GetById(Expression<Func<T, bool>> expression);
    void Insert(T elem);
    void Delete(T elem);
    void Update(T elem);
    void Save();

    IEnumerable<T> GetByText(string text);
}