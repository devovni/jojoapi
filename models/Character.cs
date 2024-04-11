using System.Text.Json.Serialization;
using NpgsqlTypes;

namespace Jojo.Models;
public class Character
{
    public int Char_id { get; set; }
    public string? FullName { get; set; }
    [JsonConverter(typeof(DateOnlyJsonConverter))]
    public DateTime Birthdate { get; set; }
    [JsonConverter(typeof(JsonStringEnumConverter<Sex>))]
    public Sex Sex { get; set; }
    public int Height_in_cm { get; set; }
    public int Weight_in_kg { get; set; }
    public string? Nationality { get; set; }
    public ICollection<string>? Appareances { get; set; }
    public Stand? Stand { get; set; }
}

public enum Sex
{
    [PgName("Unknown")]
    Unknown,
    [PgName("Male")]
    Male,
    [PgName("Female")]
    Female,
    [PgName("NotApplicable")]
    NotApplicable,

}