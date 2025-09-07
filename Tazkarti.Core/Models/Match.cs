namespace Tazkarti.Core.Models;

public class Match
{
    public int Id { get; set; }
    
    public List<string> Teams {get; set;}
    
    public string Stadium {get; set;}
    
    public string? Group {get; set;}
    
    public string? Stage {get; set;}
    
    public DateTime Date { get; set; }
    
    public int Price { get; set; }
    
    public List<Ticket>? Tickets { get; set; }
}