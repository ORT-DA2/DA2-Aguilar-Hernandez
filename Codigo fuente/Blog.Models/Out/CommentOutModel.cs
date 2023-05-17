using Blog.Domain.Entities;

namespace Blog.Models.Out;

public class CommentOutModel
{
    public Guid Id { get; set; }
    public string OwnerUsername { get; set; }
    public string Article { get; set; }
    public string Body { get; set; }
    public string? Reply { get; set; }
    public DateTime DatePublished { get; set; }
    public bool IsPublic { get; set; }
    public bool IsApproved { get; set; }
    public bool IsEdited { get; set; }
    public IEnumerable<OffensiveWord> OffensiveContent { get; set; }
    public CommentOutModel(Comment comment)
    {
        Id = comment.Id;
        OwnerUsername = comment.Owner.Username;
        Article = comment.Article.Title;
        Body = comment.Body;
        Reply = comment.Reply;
        DatePublished = comment.DatePublished;
        IsPublic = comment.IsPublic;
        IsApproved = comment.IsApproved;
        IsEdited = comment.IsEdited;
        OffensiveContent = comment.OffensiveContent;
    }
}