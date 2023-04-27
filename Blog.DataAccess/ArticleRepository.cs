using Blog.Domain.Entities;

namespace Blog.DataAccess;

public class ArticleRepository: Repository<Article>
{
    public ArticleRepository(BlogDbContext context) : base(context)
    {
    }
    
    public override  IEnumerable<Article> GetByText(string text)
    {
        throw new NotImplementedException();
    }
}