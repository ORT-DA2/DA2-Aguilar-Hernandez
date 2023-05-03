namespace Blog.Domain.Entities;

public class Notification
{
    public Guid Id { get; set; }
    public User UserToNotify { get; set; }
    public Comment Comment { get; set; }
    public bool IsRead { get; set; }
}