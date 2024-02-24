namespace Jojo.Models;
public class Character
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public DateOnly Birthday { get; set; }
    public required string Gender { get; set; }
    public int Height { get; set; }
    public int Weight { get; set; }
    public required string Nationality { get; set; }
    public required string Appearances { get; set; }
    public Stand? Stand { get; set; }
}