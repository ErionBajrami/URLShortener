using System.ComponentModel.DataAnnotations;

namespace URLShortener.DTOs;

public class LoginModel
{
    [EmailAddress]
    public string Email { get; set; }
    
    public string Password { get; set; }
}