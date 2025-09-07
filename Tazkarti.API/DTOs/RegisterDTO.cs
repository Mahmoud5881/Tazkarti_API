namespace Tazkarti.API.DTOs;

public class RegisterDTO
{
    public string FirstName { get; set; }
    
    public string LastName { get; set; }
    
    public string Username { get; set; }
    
    public string Email { get; set; }
    
    public string Password { get; set; }
    
    public string ConfirmPassword { get; set; }
    
    public string PhoneNumber { get; set; }
    
    public string City { get; set; }
    
    public string Region { get; set; }
        
    public string Language { get; set; }

    public DateTime BirthDate { get; set; }
}