using Blog.Domain.Importer;

namespace Blog.Models.In;

public class ParameterInModel
{
    public ParameterType ParameterType { get; set; }
    public string Name { get; set; }
    public object Value { get; set; }

    public Parameter ToEntity()
    {
        return new Parameter()
        {
            ParameterType = ParameterType,
            Name = Name,
            Value = Value
        };
    }
}