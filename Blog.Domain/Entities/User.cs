using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;
using Microsoft.EntityFrameworkCore;

namespace Blog.Domain.Entities;

[Index(nameof(Username), IsUnique = true)]
public class User
{
    [Required]
    public Guid Id { get; set; }
    [Required]
    public string FirstName { get; set; }
    [Required]
    public string LastName { get; set; }
    [Required]
    [MaxLength(12)]
    [MinLength(4)]
    [RegularExpression(@"^\w+$")]
    public string Username { get; set; }
    [Required]
    [MaxLength(16)]
    [MinLength(5)]
    [PasswordPropertyText(true)]
    public string Password { get; set; }
    [Required]
    public ICollection<UserRole> Roles { get; set; }
    [Required]
    [EmailAddress]
    [RegularExpression(@"^\w+([\.-]?\w+)*@\w+([\.-]?\w+)*(\.\w{2,3})+$")]
    public string Email { get; set; }

    public void ValidateEmptyString()
    {
        if (String.IsNullOrEmpty(FirstName))
            throw new ArgumentException("Empty FirstName");
        if (String.IsNullOrEmpty(LastName))
            throw new ArgumentException("Empty LastName");
        if (String.IsNullOrEmpty(Username))
            throw new ArgumentException("Empty Username");
        if (String.IsNullOrEmpty(Password))
            throw new ArgumentException("Empty Password");
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

    public void ValidateUsernameLenght()
    {
        if (Username.Length > 16 || Username.Length < 5)
        {
            throw new ArgumentException("Password must be between 16 and 5 characters");
        }
    }
    
    public void ValidatePasswordLenght()
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
}