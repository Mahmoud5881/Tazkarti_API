namespace Tazkarti.Core.Models;

public class Event
{
    public int Id { get; set; }
    
    public string Name { get; set; }
    
    public int CategoryId { get; set; }
    
    public Category Category { get; set; }
    
    public DateTime Date { get; set; }
    
    public int Price { get; set; }
    
    public string Location { get; set; }
    
    public List<Ticket>? Tickets { get; set; }
}