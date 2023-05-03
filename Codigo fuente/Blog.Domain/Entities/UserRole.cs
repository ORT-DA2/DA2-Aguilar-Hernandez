using System.ComponentModel.DataAnnotations.Schema;
using Blog.Domain.Enums;

namespace Blog.Domain.Entities;

public class UserRole
{
    public Guid UserId { get; set; }
    public virtual User User { get; set; }
    public Role Role { get; set; }
}