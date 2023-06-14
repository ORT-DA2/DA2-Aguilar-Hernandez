using Blog.Domain.Entities;
using Blog.Domain.Importer;
using Blog.ImporterInterface;

namespace Blog.JsonImporter;

public class JsonImporter : IImporter
{
    public JsonImporter(){}
    public string GetName()
    {
        return "JsonImporter";
    }

    public List<Parameter> GetParameters()
    {
        return new List<Parameter>()
        {
            new Parameter()
            {
                Name = "filename",
                Necessary = true,
                ParameterType = ParameterType.Text
            },
        };
    }

    public List<Article> ImportArticles(List<Parameter> parameters)
    {
        var fileName = parameters.Find(p => p.Name == "File Name");
        var parsedName = fileName.Value.ToString();

        //var file = Directory.Load(parsedName);
        //var articlesImported = NewtonSoft.ParseJson(file);
        
        return new List<Article>();
    }

}