namespace Jojo.Models;

public class Stand
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string LocalizedName { get; set; }
    public Character User { get; set; }
    public string Type { get; set; }
    public bool Humanoid { get; set; }
    
}