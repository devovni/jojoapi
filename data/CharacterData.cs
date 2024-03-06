using Jojo.Models;
using Dapper;

namespace Jojo.Data;

public class CharacterData(IDataContext db) : ICharacterData
{
    private readonly IDataContext _db = db;

    public async Task<IEnumerable<Character>> GetAll()
    {
        string sqlquery = @"SELECT * FROM characters
                            left JOIN stands ON characters.char_id = stands.char_id
                            left JOIN appareances ON characters.char_id = appareances.char_id
                            left JOIN parts ON appareances.part_id = parts.part_id
                            ";
        var db = _db.DbConnection();
        var result = await db.QueryAsync<Character, Stand, Appareances, Part, Character>(sqlquery, map: (Character, Stand, Appareances, Part) =>
        {
            Character.FullName = Character.FullName.Trim();
            Character.Stand = Stand;
            if(Stand != null)
                Stand.User = Character.FullName;
            Character.Appareances = [Part.Name];
            return Character;
            
        }, splitOn:"stand_id, char_id, part_id");

        return result.GroupBy(x => x.Char_id).Select(y =>
        {
            var grouped = y.First();
            grouped.Appareances = y.Select(x => x.Appareances.Single()).ToList();
            return grouped;
        });

    }
}