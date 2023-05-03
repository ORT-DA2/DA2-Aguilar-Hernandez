namespace Blog.Domain.Entities;

public class Session
{
    public Guid Id { get; set; }
    public Guid AuthToken { get; set; }
    public User User { get; set; }

    public Session()
    {
        AuthToken = Guid.NewGuid();
    }
}