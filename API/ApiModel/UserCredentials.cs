using System.ComponentModel.DataAnnotations;

namespace API.ApiModel;

public class UserCredentials
{
    [Required]
    public string Email { get; set; }
    [Required] 
    public string Password { get; set; }
}