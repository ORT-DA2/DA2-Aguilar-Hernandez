namespace Blog.Domain.Entities;

public class OffensiveWord
{
    public int Id { get; set; }
    public string Word { get; set; }
    public ICollection<Article> Articles { get; set; } = new List<Article>();
    public ICollection<Comment> Comments { get; set; } = new List<Comment>();
}