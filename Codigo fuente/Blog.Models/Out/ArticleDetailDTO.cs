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
    public string? Image2 { get; set; }
    public Template Template { get; set; }
    public bool IsApproved { get; set; }
    public bool IsEdited { get; set; }
    public IEnumerable<OffensiveWord> OffensiveContent { get; set; }

    public ArticleDetailDTO(Article article)
    {
        List<CommentOutModel> comments = new List<CommentOutModel>();
        if (article.Comments != null)
        {
            comments.AddRange(article.Comments.Select(comment => new CommentOutModel(comment)));
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
        if (!string.IsNullOrEmpty(article.Image2))
            Image2 = article.Image2;
        Template = article.Template;
        IsApproved = article.IsApproved;
        IsEdited = article.IsEdited;
        OffensiveContent = article.OffensiveContent;
    }
}