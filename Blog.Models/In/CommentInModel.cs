using Blog.Domain.Entities;

namespace Blog.Models.In;

public class CommentInModel
{
    public Domain.Entities.User Owner { get; set; }
    public Domain.Entities.Article Article { get; set; }
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