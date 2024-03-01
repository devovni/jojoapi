using Jojo.Models;
using Dapper;
using Npgsql;

namespace Jojo.Data;

public class CharacterData(IDataContext db) : ICharacterData
{
    private readonly IDataContext _db = db;

    public async Task<IEnumerable<Character>> GetAll()
    {
        string sqlquery = "select * from characters left join stands using(char_id);";
        var db = _db.DbConnection();
        return await db.QueryAsync<Character, Stand, Character>(sqlquery, map: (Character, Stand) =>{
            Character.Stand = Stand;
            return Character;
        }, splitOn: "stand_id");


    }
    // public Task<IEnumerable<Character>> InsertCharacter(Character character) => _db.SaveData<Character>("get_all", character);

}   