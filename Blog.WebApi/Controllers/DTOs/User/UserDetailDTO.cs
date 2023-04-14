using Blog.Domain.Entities;
using Blog.Domain.Enums;

namespace Blog.WebApi.Controllers.DTOs;

public class UserDetailDTO
{
    public Guid Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Username { get; set; }
    public string Password { get; set; }
    public ICollection<UserRole> Roles { get; set; }
    public string Email { get; set; }

    public UserDetailDTO(User user)
    {
        Id = user.Id;
        FirstName = user.FirstName;
        LastName = user.LastName;
        Username = user.Username;
        Password = user.Password;
        Roles = user.Roles;
        Email = user.Email;
    }
}