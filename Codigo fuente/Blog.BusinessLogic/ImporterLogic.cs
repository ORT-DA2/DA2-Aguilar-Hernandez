using System.Reflection;
using System.Reflection.Metadata;
using Blog.Domain.Entities;
using Blog.Domain.Exceptions;
using Blog.ImporterInterface;
using Parameter = Blog.Domain.Importer.Parameter;

namespace Blog.BusinessLogic;

public class ImporterLogic
{
    public List<IImporter> GetAllImporters()
    {
        return GetImporterImplementations();
    }

    public List<Article> ImportArticles(string importerName, List<Parameter> parameters)
    {
        List<IImporter> importers = GetImporterImplementations();
        IImporter? desiredImplementation = null;

        foreach (IImporter importer in importers)
        {
            if (importer.GetName() == importerName)
            {
                desiredImplementation = importer;
                break;
            }
        }

        if (desiredImplementation == null)
            throw new NotFoundException("No se pudo encontrar el importador solicitado");

        List<Article> importedArticles = desiredImplementation.ImportArticles(parameters);
        return importedArticles;
    }

    private List<IImporter> GetImporterImplementations()
    {
        List<IImporter> availableImporters = new List<IImporter>();
        string importersPath = "./Importers";
        string[] filePaths = Directory.GetFiles(importersPath);

        foreach (string filePath in filePaths)
        {
            if (filePath.EndsWith(".dll"))
            {
                FileInfo fileInfo = new FileInfo(filePath);
                Assembly assembly = Assembly.LoadFile(fileInfo.FullName);
                
                foreach (Type type in assembly.GetTypes())
                {
                    if (typeof(IImporter).IsAssignableFrom(type) && !type.IsInterface)
                    {
                        IImporter importer = (IImporter)Activator.CreateInstance(type);
                        if (importer != null)
                            availableImporters.Add(importer);
                    }
                }
            }
        }

        return availableImporters;
    }

}