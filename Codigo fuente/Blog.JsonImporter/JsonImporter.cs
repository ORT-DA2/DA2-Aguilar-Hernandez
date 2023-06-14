using Blog.Domain.Entities;
using Blog.Domain.Importer;
using Blog.ImporterInterface;
using Newtonsoft.Json.Linq;

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
                Name = "File Name",
                Necessary = true,
                ParameterType = ParameterType.Text
            },
        };
    }

    public List<Article> ImportArticles(List<Parameter> parameters)
    {
        
        var fileName = parameters.Find(p => p.Name == "File Name");
        var parsedName = fileName?.Value.ToString();
        JObject jsonParsed = JObject.Parse(File.ReadAllText( parsedName));
        JArray arrayArticles = (JArray)jsonParsed["articles"];

        return arrayArticles.ToObject<List<Article>>();
        
    }

}