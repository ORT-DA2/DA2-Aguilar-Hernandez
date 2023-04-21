using Blog.Domain.Entities;

namespace Blog.Models.In;

public class CommentInModel
{
    public User Owner { get; set; }
    public Article Article { get; set; }
    public string Body { get; set; }
    public string? Reply { get; set; }

    public Comment ToEntity()
    {
        return new Comment()
        {
            Owner = this.Owner,
            Article = this.Article,
            Body = this.Body,
            Reply = this.Reply
        };
    }
}