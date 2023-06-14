using Blog.Domain.Entities;
using Blog.Domain.Importer;

namespace Blog.ImporterInterface;

public interface IImporter
{   
    string GetName();
    List<Parameter> GetParameters();
    List<Article> ImportArticles(List<Parameter> parameters);
}