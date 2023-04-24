using Blog.Domain.Entities;
using Blog.Domain.Enums;
using Blog.Models.In.UserRole;

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
    public byte[]? Image { get; set; }
    public Template Template { get; set; }
    
    public Domain.Entities.Article ToEntity()
    {

        return new Domain.Entities.Article()
        {
            Title = Title,
            Content = Content,
            IsPublic = IsPublic,
            Owner = Owner,
            DatePublished = DatePublished,
            DateLastModified = DateLastModified,
            Comments = Comments,
            Image = Image,
            Template = Template
            
        };
    }
}