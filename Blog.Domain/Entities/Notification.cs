namespace Blog.Domain.Entities;

public class Notification
{
    public Guid Id { get; set; }
    public User Owner { get; set; }
    public Article Article { get; set; }
    public Comment Comment { get; set; }
}