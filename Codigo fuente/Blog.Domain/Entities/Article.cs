using Blog.Domain.Enums;

namespace Blog.Domain.Entities;

public class Article
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public string Content { get; set; }
    public bool IsPublic { get; set; }
    public User Owner { get; set; }
    public DateTime DatePublished { get; set; }
    public DateTime DateLastModified { get; set; }
    public List<Comment>? Comments { get; set; }
    public string Image { get; set; }
    public Template Template { get; set; }
    public bool IsApproved { get; set; }
    public bool IsEdited { get; set; }
    public IEnumerable<OffensiveWord> OffensiveContent { get; set; }
    
    public void UpdateAttributes(Article article)
    {
        Title = article.Title;
        Content = article.Content;
        IsPublic = article.IsPublic;
        Owner = article.Owner;
        DatePublished = article.DatePublished;
        DateLastModified = article.DateLastModified;
        Comments = article.Comments;
        Image = article.Image;
        Template = article.Template;
        IsApproved = article.IsApproved;
        IsEdited = article.IsEdited;
        OffensiveContent = article.OffensiveContent;
    }
}