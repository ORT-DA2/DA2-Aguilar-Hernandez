using Blog.Domain.Enums;

namespace Blog.Models.In;

public class RegisterDto
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Username { get; set; }
    public string Password { get; set; }
    public string Email { get; set; }
    
    public Domain.Entities.User ToEntity()
    {
        var roles = new List<UserRoleBasicInfoDTO>()
        {
            new UserRoleBasicInfoDTO()
            {
                Role = Role.Blogger
            }
        };
        var rolList = roles.Select(rol => rol.ToEntity()).ToList();

        return new Domain.Entities.User()
        {
            FirstName = FirstName,
            LastName = LastName,
            Username = Username,
            Password = Password,
            Email = Email,
            Roles = rolList
        };
    }
}