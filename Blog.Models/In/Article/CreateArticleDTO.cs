using Blog.Domain.Entities;
using Blog.Domain.Enums;
using System.IO;

namespace Blog.Models.In.Article;

public class CreateArticleDTO
{
    public string Title { get; set; }
    public string Content { get; set; }
    public bool IsPublic { get; set; }
    public Domain.Entities.User Owner { get; set; }
    public DateTime DatePublished { get; set; }
    public DateTime DateLastModified { get; set; }
    public List<Comment>? Comments { get; set; }
    public string Image { get; set; }
    public Template Template { get; set; }
    
    public Domain.Entities.Article ToEntity(string image)
    {
        byte[] imageBytes = null;

        if (image != null)
        {
            using var ms = new MemoryStream();
            imageBytes = ms.ToArray();
        }
        

        return new Domain.Entities.Article()
        {
            Title = Title,
            Content = Content,
            IsPublic = IsPublic,
            Owner = Owner,
            DatePublished = DatePublished,
            DateLastModified = DateLastModified,
            Comments = Comments,
            Image = imageBytes,
            Template = Template
            
        };
    }
}