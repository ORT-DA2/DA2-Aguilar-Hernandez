﻿namespace Blog.Models.In;

public class CreateUserDTO
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Username { get; set; }
    public string? Password { get; set; }
    public ICollection<UserRoleBasicInfoDTO> Roles { get; set; }
    public string Email { get; set; }
    
    public Domain.Entities.User ToEntity(ICollection<UserRoleBasicInfoDTO> roles)
    {
        var rolList = new List<Domain.Entities.UserRole>();
        foreach (var rol in roles)
        {
            rolList.Add(rol.ToEntity());
        }
        if(Password == null)
            return new Domain.Entities.User()
            {
                FirstName = FirstName,
                LastName = LastName,
                Username = Username,
                Email = Email,
                Roles = rolList
            };
        
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