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

    public bool ValidateEmptyString(string str)
    {
        return String.IsNullOrEmpty(str);
    }
    
    public bool ValidateAlfanumeric(string str)
    {
        Regex regex = new Regex(@"^\w+$", RegexOptions.IgnoreCase);
        return regex.IsMatch(str);
    }
    
    public bool ValidateEmail(string email)
    {
        Regex regex = new Regex(@"^\w+([\.-]?\w+)*@\w+([\.-]?\w+)*(\.\w{2,3})+$", RegexOptions.IgnoreCase);
        return regex.IsMatch(email);
    }
}