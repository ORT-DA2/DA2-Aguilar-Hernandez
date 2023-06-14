using Blog.Domain.Importer;
using Blog.ImporterInterface;

namespace Blog.Models.Out;

public class ImportOutModel
{
    public string ImporterName { get; set; }
    public List<ParameterOutModel> Parameters { get; set; }
    
    public ImportOutModel(IImporter importer)
    {
        ImporterName = importer.GetName();
        Parameters = importer.GetParameters()
            .Select(p => new ParameterOutModel(p)).ToList();
    }
}