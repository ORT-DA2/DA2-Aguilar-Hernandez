using Blog.Domain.Entities;
using Blog.WebApi.Controllers.DTOs.UserRole;

namespace Blog.WebApi.Controllers.DTOs;

public class UserDetailDTO
{
    public Guid Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Username { get; set; }
    public string Password { get; set; }
    public ICollection<UserRoleBasicInfoDTO> Roles { get; set; } = new List<UserRoleBasicInfoDTO>();
    public string Email { get; set; }

    public UserDetailDTO(User user)
    {
        Id = user.Id;
        FirstName = user.FirstName;
        LastName = user.LastName;
        Username = user.Username;
        Password = user.Password;
        Email = user.Email;
        if (user.Roles != null)
        {
            foreach (var userRole in user.Roles)
            {
                var urBasic = new UserRoleBasicInfoDTO(userRole);
                Roles.Add(urBasic);
            }
        }
        
    }
}