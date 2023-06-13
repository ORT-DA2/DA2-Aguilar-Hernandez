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
    public string Template { get; set; }
    
    public Article ToEntity()
    {
        Article article = new Article()
        {
            Title = Title,
            Content = Content,
            Image = Image,
            Image2 = Image2,
            IsPublic = IsPublic,
            Template = parseTemplateToEnum(Template)
        };

        return article;
    }

    private Template parseTemplateToEnum(string template)
    {
        switch (template)
        {
            case "Rectangle at Top":
                return Domain.Enums.Template.RectangleTop;
            case "Rectangle at Bottom":
                return Domain.Enums.Template.RectangleBottom;
            case "Square at Top Left":
                return Domain.Enums.Template.SquareTopLeft;
            case "Rectangle at Top and Bottom":
                return Domain.Enums.Template.RectangleTopBottom;
        }

        return Domain.Enums.Template.RectangleTop;
    }
}