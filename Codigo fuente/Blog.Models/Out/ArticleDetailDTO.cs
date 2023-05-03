using Blog.Domain.Entities;
using Blog.Domain.Enums;

namespace Blog.Models.Out;

public class ArticleDetailDTO
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public string Content { get; set; }
    public bool IsPublic { get; set; }
    public string Owner { get; set; }
    public DateTime DatePublished { get; set; }
    public DateTime DateLastModified { get; set; }
    public List<CommentOutModel>? Comments { get; set; }
    public string Image { get; set; }
    public Template Template { get; set; }

    public ArticleDetailDTO(Domain.Entities.Article article)
    {
        List<CommentOutModel> comments = new List<CommentOutModel>();
        if (article.Comments != null)
        {
            foreach (var comment in article.Comments)
            {
                CommentOutModel commentOut = new CommentOutModel(comment);
                comments.Add(commentOut);
            }
        }
        
        Id = article.Id;
        Title = article.Title;
        Content = article.Content;
        IsPublic = article.IsPublic;
        Owner = article.Owner.Username;
        DatePublished = article.DatePublished;
        DateLastModified = article.DateLastModified;
        Comments = comments;
        Image = article.Image;
        Template = article.Template;
    }
}