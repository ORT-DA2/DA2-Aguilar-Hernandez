using Blog.Domain.Entities;

namespace Blog.Models.In;

public class CommentInModel
{
    public Guid OwnerId { get; set; }
    public Guid ArticleId { get; set; }
    public string Body { get; set; }
    public string? Reply { get; set; }
    public DateTime DatePublished { get; set; }
    
    public Comment ToEntity()
    {
        return new Comment()
        {
            Body = this.Body,
            Reply = this.Reply,
            DatePublished = this.DatePublished
        };
    }
}