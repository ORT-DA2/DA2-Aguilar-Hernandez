using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.RegularExpressions;
using Blog.Domain.Enums;
using Microsoft.EntityFrameworkCore;

namespace Blog.Domain.Entities;

[Index(nameof(Username), IsUnique = true)]
[Index(nameof(Email), IsUnique = true)]
public class User
{
    [Required]
    public Guid Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    [MaxLength(12)]
    [MinLength(4)]
    [RegularExpression(@"^\w+$")]
    public string Username { get; set; }
    [MaxLength(16)]
    [MinLength(5)]
    [PasswordPropertyText(true)]
    public string Password { get; set; }
    public virtual ICollection<UserRole> Roles { get; set; }
    [EmailAddress]
    [RegularExpression(@"^\w+([\.-]?\w+)*@\w+([\.-]?\w+)*(\.\w{2,3})+$")]
    public string Email { get; set; }

    public List<Comment>? Comments { get; set; }
    
    
    public void FirstNameValidation()
    {
        if (String.IsNullOrEmpty(FirstName))
            throw new ArgumentException("Empty FirstName");
    }
    
    public void LastNameValidation()
    {
        if (String.IsNullOrEmpty(LastName))
            throw new ArgumentException("Empty LastName");
    }
    
    public void UsernameValidation()
    {
        if (String.IsNullOrEmpty(Username))
            throw new ArgumentException("Empty Username");
    }
    
    public void PasswordValidation()
    {
        if (String.IsNullOrEmpty(Password))
            throw new ArgumentException("Empty Password");
    }
    
    public void EmailValidation()
    {
        if (String.IsNullOrEmpty(Email))
            throw new ArgumentException("Empty Email");
    }
    
    public void ValidateAlfanumericUsername()
    {
        Regex regex = new Regex(@"^\w+$", RegexOptions.IgnoreCase);
        if (!regex.IsMatch(Username))
        {
            throw new ArgumentException("Username must be alphanumeric");
        }
    }

    public void ValidatePasswordLenght()
    {
        if (Password.Length > 16 || Password.Length < 5)
        {
            throw new ArgumentException("Password must be between 16 and 5 characters");
        }
    }
    
    public void ValidateUsernameLenght()
    {
        if (Username.Length > 12 || Username.Length < 4)
        {
            throw new ArgumentException("Username must be between 12 and 4 characters");
        }
    }
    
    public void ValidateEmail()
    {
        Regex regex = new Regex(@"^\w+([\.-]?\w+)*@\w+([\.-]?\w+)*(\.\w{2,3})+$", RegexOptions.IgnoreCase);
        if (!regex.IsMatch(Email))
        {
            throw new ArgumentException("Invalid Email");
        }
    }
    
    public void UpdateAttributes(User user)
    {
        FirstName = user.FirstName;
        LastName = user.LastName;
        Username = user.Username;
        Roles = user.Roles;
        Email = user.Email;
        Comments = user.Comments;
        if (user.Password != null)
        {
            Password = user.Password;
        }
    }

    public bool IsInRole(Role role)
    {
       return Roles.Any(ur => ur.Role == role);
    }
}