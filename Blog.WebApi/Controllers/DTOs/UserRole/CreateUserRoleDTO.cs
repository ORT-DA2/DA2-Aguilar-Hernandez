using Blog.Domain.Entities;
using Blog.Domain.Enums;

namespace Blog.WebApi.Controllers.DTOs.UserRole;

public class CreateUserRoleDTO
{
    public Role Role { get; set; }
    
    public Domain.Entities.UserRole ToEntity()
    {
        return new Domain.Entities.UserRole()
        {
            Role = Role
        };
    }
}