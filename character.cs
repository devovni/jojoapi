namespace Jojo.Models;
public class Character
{
    public int Id { get; set; }
    public string Name { get; set; }
    public DateOnly Birthday { get; set; }
    public int Height { get; set; }
    public int Weight { get; set; }
    public string Nationality { get; set; }
    public string FirstApperance { get; set; }
    public Stand? Stand { get; set; }
}