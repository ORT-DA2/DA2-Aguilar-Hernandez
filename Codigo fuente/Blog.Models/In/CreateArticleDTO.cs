using Blog.Domain.Entities;
using Blog.Domain.Enums;
using Microsoft.AspNetCore.Http;

namespace Blog.Models.In;

public class CreateArticleDTO
{
    public string Title { get; set; }
    public string Content { get; set; }
    public bool IsPublic { get; set; }
    public string Image { get; set; }
    public string? Image2 { get; set; }
    public Template Template { get; set; }
    
    public Article ToEntity()
    {
        Article article = new Article()
        {
            Title = Title,
            Content = Content,
            Image = Image,
            Image2 = Image2,
            IsPublic = IsPublic,
            Template = Template
        };

        return article;
    }
}