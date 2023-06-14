using System.Reflection;
using System.Reflection.Metadata;
using Blog.Domain.Entities;
using Blog.Domain.Enums;
using Blog.Domain.Exceptions;
using Blog.IBusinessLogic;
using Blog.ImporterInterface;
using Microsoft.AspNetCore.Hosting;
using Parameter = Blog.Domain.Importer.Parameter;

namespace Blog.BusinessLogic;

public class ImporterLogic : IImporterLogic
{
    private IArticleLogic _articleLogic;
    private readonly IWebHostEnvironment _hostEnvironment;

    public ImporterLogic(IArticleLogic articleLogic, IWebHostEnvironment hostEnvironment)
    {
        _articleLogic = articleLogic;
        _hostEnvironment = hostEnvironment;
    }
    public List<IImporter> GetAllImporters()
    {
        return GetImporterImplementations();
    }

    public List<Article> ImportArticles(string importerName, List<Parameter> parameters, Guid authToken)
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
        foreach (Article article in importedArticles)
        {
            article.Template = Template.RectangleTop;
            article.IsPublic = true;
            article.IsApproved = true;
            article.IsEdited = false;
            article.Id = Guid.NewGuid();
            string image = ImportImage(article.Image);
            article.Image = image;
            _articleLogic.CreateArticle(article, authToken);
        }
        return importedArticles;
    }

    private string ImportImage(string image)
    {
        byte[] imageBytes = Convert.FromBase64String(image);
        var imageFolderPath = Path.Combine(_hostEnvironment.WebRootPath, "images");

        if (!Directory.Exists(imageFolderPath))
        {
            Directory.CreateDirectory(imageFolderPath);
        }
        var imageName = $"{Guid.NewGuid().ToString()}.jpg";
        var fullImagePath = Path.Combine(imageFolderPath, imageName);
        System.IO.File.WriteAllBytesAsync(fullImagePath, imageBytes);

        return $"images/{imageName}";
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