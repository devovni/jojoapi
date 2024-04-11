using System.ComponentModel.DataAnnotations.Schema;
using Dapper;

namespace Jojo.Models;

public class Stand
{
    public int? Stand_id { get; set; }
    public  string StandName { get; set; } = "Unknown";
    public string? LocalizedName { get; set; }
    public  string? User { get; set; } = "Unknown";
    public string? StandType { get; set; } = "Unknown";

    public bool Humanoid { get; set; } = false;
    
}