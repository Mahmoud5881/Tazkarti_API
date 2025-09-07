namespace Tazkarti.Core.Models;

public class Ticket
{
    public int Id { get; set; }
    
    public int? EventId { get; set; }
    
    public Event? Event { get; set; }
    
    public int? MatchId { get; set; }
    
    public Match? Match { get; set; }
    
    public string? UserId { get; set; }
    
    public AppUser? User { get; set; }
}