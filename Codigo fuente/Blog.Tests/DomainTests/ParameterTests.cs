using Blog.Domain.Importer;

namespace Blog.Tests.DomainTests;

[TestClass]
public class ParameterTests
{
    
    [TestMethod]
    public void GetAndSetParameterTest()
    {
        Parameter parameter = new Parameter()
        {
            Name = "name",
            Necessary = true,
            ParameterType = ParameterType.Text,
            Value = "articles.json"
        };
        
        Assert.AreEqual("name", parameter.Name);
        Assert.AreEqual(true, parameter.Necessary);
        Assert.AreEqual(ParameterType.Text, parameter.ParameterType);
        Assert.AreEqual("articles.json", parameter.Value);
        
    }
    
}