using System.Linq.Expressions;
using Blog.Domain.Entities;

namespace Blog.IDataAccess;

public interface IRepository<T> where T : class
{
    IEnumerable<T> GetAll();
    T? GetBy(Expression<Func<T, bool>> expression);
    void Insert(T elem);
    void Delete(T elem);
    void Update(T elem);
    void Save();

    IEnumerable<T> GetByText(string text);
    IEnumerable<T> GetLastTen();
    
    IEnumerable<T> GetByUser(User user);
    IEnumerable<T> GetPublicAll();
    IEnumerable<T> GetUserArticles(string username);
    Dictionary<T, int> GetUserByActivity(DateTime startDate, DateTime endDate);

}