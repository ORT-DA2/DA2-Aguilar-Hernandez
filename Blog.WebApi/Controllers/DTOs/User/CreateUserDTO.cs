using Blog.Domain.Entities;
using Blog.WebApi.Controllers.DTOs.UserRole;

namespace Blog.WebApi.Controllers.DTOs;

public class CreateUserDTO
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Username { get; set; }
    public string Password { get; set; }
    public ICollection<UserRoleBasicInfoDTO> Roles { get; set; }
    public string Email { get; set; }
    
    public User ToEntity(List<Domain.Entities.UserRole> roles)
    {
        
        return new User()
        {
            FirstName = FirstName,
            LastName = LastName,
            Username = Username,
            Password = Password,
            Email = Email,
            Roles = roles
            
        };
    }
}