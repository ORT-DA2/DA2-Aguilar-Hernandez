using Blog.Domain.Entities;

namespace Blog.Models.In;

public class CommentInModel
{
    public Guid Owner { get; set; }
    public Guid Article { get; set; }
    public string Body { get; set; }
    public string? Reply { get; set; }

    public Comment ToEntity()
    {
        return new Comment()
        {
            Body = this.Body,
            Reply = this.Reply
        };
    }
}