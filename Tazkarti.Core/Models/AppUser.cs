using Microsoft.AspNetCore.Identity;

namespace Tazkarti.Core.Models;

public class AppUser : IdentityUser
{
    public string FirstName { get; set; }
    
    public string LastName { get; set; }
    
    public string City { get; set; }
    
    public string Region { get; set; }
    
    public string Language { get; set; }
    
    public DateTime BirthDate { get; set; }
    
    public List<Ticket>? Tickets { get; set; }
}