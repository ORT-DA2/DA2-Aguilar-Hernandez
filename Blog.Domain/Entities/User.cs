using System.Text.RegularExpressions;
using Blog.Domain.Enums;

namespace Blog.Domain.Entities;

public class User
{
    public Guid Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Username { get; set; }
    public string Password { get; set; }
    public Role Role { get; set; }
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