using Blog.Domain.Entities;
using Blog.Domain.Importer;
using Blog.ImporterInterface;

namespace Blog.IBusinessLogic;

public interface IImporterLogic
{
    
    List<IImporter> GetAllImporters();

    List<Article> ImportArticles(string importerName, List<Parameter> parameters, Guid authToken);

}