using Blog.Domain.Entities;
using Blog.Domain.Enums;

namespace Blog.WebApi.Controllers.DTOs;

public class CreateUserDTO
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Username { get; set; }
    public string Password { get; set; }
    public string Email { get; set; }
    
    public User ToEntity()
    {
        return new User()
        {
            FirstName = FirstName,
            LastName = LastName,
            Username = Username,
            Password = Password,
            Email = Email,
            Roles = new List<UserRole>{}
        };
    }
}