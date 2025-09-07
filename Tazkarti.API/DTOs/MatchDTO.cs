namespace Tazkarti.API.DTOs;

public class MatchDTO
{
    public List<string> Teams {get; set;}
    
    public string Stadium {get; set;}
    
    public string? Group {get; set;}
    
    public string? Stage {get; set;}
    
    public DateTime Date { get; set; }
    
    public int Price { get; set; }
}