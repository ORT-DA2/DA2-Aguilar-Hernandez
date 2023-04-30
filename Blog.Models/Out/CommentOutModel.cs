using Blog.Domain.Entities;

namespace Blog.Models.Out;

public class CommentOutModel
{
    public Guid Id { get; set; }
    public Domain.Entities.User Owner { get; set; }
    public Domain.Entities.Article Article { get; set; }
    public string Body { get; set; }
    public string? Reply { get; set; }
    public DateTime DatePublished { get; set; }
    public CommentOutModel(Comment comment)
    {
        Id = comment.Id;
        Owner = comment.Owner;
        Article = comment.Article;
        Body = comment.Body;
        Reply = comment.Reply;
        DatePublished = comment.DatePublished;
    }
}