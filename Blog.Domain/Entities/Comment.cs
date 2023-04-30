namespace Blog.Domain.Entities;

public class Comment
{
    public Guid Id { get; set; }
    public User Owner { get; set; }
    public Article Article { get; set; }
    public string Body { get; set; }
    public DateTime DatePublished { get; set; }
    public string? Reply { get; set; }
    
}







