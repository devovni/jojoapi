namespace Jojo.Models;

public class Stand
{
    public int? Stand_id { get; set; }
    public required string Name { get; set; }
    public string? LocalizedName { get; set; }
    public required string User { get; set; }
    public required string Type { get; set; }
    public bool Humanoid { get; set; }
    
}