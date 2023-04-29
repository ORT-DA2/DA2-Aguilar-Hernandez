using Blog.Domain.Entities;
using Blog.Domain.Enums;

namespace Blog.Models.In;

public class CreateArticleDTO
{
    public string Title { get; set; }
    public string Content { get; set; }
    public bool IsPublic { get; set; }
    public List<Comment>? Comments { get; set; }
    public string Image { get; set; }
    public Template Template { get; set; }
    
    public Domain.Entities.Article ToEntity()
    {

        return new Domain.Entities.Article()
        {
            Title = Title,
            Content = Content,
            IsPublic = IsPublic,
            Comments = Comments,
            Image = Image,
            Template = Template
            
        };
    }
}