using Blog.Domain.Importer;

namespace Blog.Models.Out;

public class ParameterOutModel
{
    public ParameterType ParameterType { get; set; }
    public string Name { get; set; }
    public bool Necessary { get; set; }
    
    public ParameterOutModel(Parameter parameter)
    {
        ParameterType = parameter.ParameterType;
        Name = parameter.Name;
        Necessary = parameter.Necessary;
    }
}