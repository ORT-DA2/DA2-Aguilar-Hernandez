using Blog.Domain.Importer;
using Blog.ImporterInterface;

namespace Blog.Models.Out;

public class ImportOutModel
{
    public string Name { get; set; }
    public List<ParameterOutModel> Parameters { get; set; }
    
    public ImportOutModel(IImporter importer)
    {
        Name = importer.GetName();
        Parameters = importer.GetParameters()
            .Select(p => new ParameterOutModel(p)).ToList();
    }
}