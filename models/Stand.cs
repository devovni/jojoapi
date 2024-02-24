namespace Jojo.Models;

public class Stand
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public string? LocalizedName { get; set; }
    public required Character User { get; set; }
    public required string Type { get; set; }
    public bool Humanoid { get; set; }
    
}