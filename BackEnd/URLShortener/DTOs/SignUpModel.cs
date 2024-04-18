using System.ComponentModel.DataAnnotations;

namespace URLShortener.DTOs;

public class SignUpModel
{
    [EmailAddress]
    public string Email { get; set; }
    
    public string FullName { get; set; }
    public string Password { get; set; }
}