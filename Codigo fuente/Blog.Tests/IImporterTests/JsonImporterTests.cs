using Blog.Domain.Entities;
using Blog.Domain.Importer;
using Blog.JsonImporter;

namespace Blog.Tests.IImporterTests;

[TestClass]
public class JsonImporterTests
{
    [TestMethod]
    public void TestGetName()
    {
        JsonImporter.JsonImporter jsonImporter = new JsonImporter.JsonImporter();
        string importerName = jsonImporter.GetName();
        Assert.AreEqual("JsonImporter", importerName);

    }

    [TestMethod]
    public void GetParametersTest()
    {
        JsonImporter.JsonImporter jsonImporter = new JsonImporter.JsonImporter();
    }
    
    [TestMethod]
    public void ImportArticlesTest()
    {
        JsonImporter.JsonImporter jsonImporter = new JsonImporter.JsonImporter();
        var parameterList = new List<Parameter>();
        var importedArticles = jsonImporter.ImportArticles(parameterList);
        Assert.IsTrue(importedArticles.Count == 0);
    }
}