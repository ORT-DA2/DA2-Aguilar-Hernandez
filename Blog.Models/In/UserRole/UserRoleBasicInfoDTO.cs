using Blog.Domain.Enums;

namespace Blog.Models.In.UserRole;

public class UserRoleBasicInfoDTO
{
    public Role Role { get; set; }

    public UserRoleBasicInfoDTO()
    {
    }

    public UserRoleBasicInfoDTO(Domain.Entities.UserRole userRole)
    {
        Role = userRole.Role;
    }
    
    public Domain.Entities.UserRole ToEntity()
    {
        return new Domain.Entities.UserRole()
        {
            Role = Role
        };
    }
}