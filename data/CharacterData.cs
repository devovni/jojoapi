using Jojo.Models;
using Dapper;

namespace Jojo.Data;

public class CharacterData(IDataContext db) : ICharacterData
{
    private readonly IDataContext _db = db;

    public async Task<IEnumerable<Character>> Get(int? id)
    {
        string sqlquery = "";
        if (id == null)
        {
            sqlquery = @"SELECT birthdate::date, * FROM characters 
                            left JOIN stands ON characters.char_id = stands.char_id
                            left JOIN appareances ON characters.char_id = appareances.char_id
                            left JOIN parts ON appareances.part_id = parts.part_id";
        }
        else
        {
            sqlquery = @"SELECT * FROM characters
                            left JOIN stands ON characters.char_id = stands.char_id
                            left JOIN appareances ON characters.char_id = appareances.char_id
                            left JOIN parts ON appareances.part_id = parts.part_id
                            WHERE characters.char_id = @id";
        }

        var db = _db.DbConnection();

        var result = await db.QueryAsync<Character, Stand, Appareances, Part, Character>(sqlquery, param: new { id }, map: (Character, Stand, Appareances, Part) =>
        {
            Character.FullName = Character.FullName!.Trim();
            Character.Stand = Stand;
            if (Stand != null)
                Stand.User = Character.FullName;
            Character.Appareances = [Part.Name];
            return Character;

        }, splitOn: "stand_id, char_id, part_id");

        return result.GroupBy(x => x.Char_id).Select(y =>
        {
            var grouped = y.First();
            grouped.Appareances = y.Select(x => x.Appareances!.First()).ToList();
            return grouped;
        });

    }

    public async Task Insert(Character character)
    {
        var db = _db.DbConnection();

        var InsertParams = new DynamicParameters();
        InsertParams.Add("@in_fullname", character.FullName, dbType: DbType.String);
        InsertParams.Add("@in_birthdate", character.Birthdate, dbType: DbType.Date);
        InsertParams.Add("@in_sex", character.Sex, dbType: DbType.Int32);
        InsertParams.Add("@in_height_in_cm", character.Height_in_cm, dbType: DbType.Int32);
        InsertParams.Add("@in_weight_in_kg", character.Weight_in_kg, dbType: DbType.Int32);
        InsertParams.Add("@in_nationality", character.Nationality, dbType: DbType.String);
        InsertParams.Add("@in_stand_name", character.Stand?.StandName, dbType: DbType.String);
        InsertParams.Add("@in_localizedname", character.Stand?.LocalizedName, dbType: DbType.String);
        InsertParams.Add("@in_stand_type", character.Stand?.StandType, dbType: DbType.String);
        InsertParams.Add("@in_humanoid", character.Stand?.Humanoid);
        InsertParams.Add("@in_appareances", character.Appareances);

        await db.ExecuteAsync("insert_character", InsertParams, commandType: CommandType.StoredProcedure);

    }


    public async Task Update(Character character)
    {
        var db = _db.DbConnection();

        var UpdateParams = new DynamicParameters();
        UpdateParams.Add("@in_char_id", character.Char_id, dbType: DbType.Int32);
        UpdateParams.Add("@in_fullname", character.FullName, dbType: DbType.String);
        UpdateParams.Add("@in_birthdate", character.Birthdate, dbType: DbType.Date);
        UpdateParams.Add("@in_sex", character.Sex, dbType: DbType.Int32);
        UpdateParams.Add("@in_height_in_cm", character.Height_in_cm, dbType: DbType.Int32);
        UpdateParams.Add("@in_weight_in_kg", character.Weight_in_kg, dbType: DbType.Int32);
        UpdateParams.Add("@in_nationality", character.Nationality, dbType: DbType.String);
        UpdateParams.Add("@in_stand_name", character.Stand?.StandName, dbType: DbType.String);
        UpdateParams.Add("@in_localizedname", character.Stand?.LocalizedName, dbType: DbType.String);
        UpdateParams.Add("@in_stand_type", character.Stand?.StandType, dbType: DbType.String);
        UpdateParams.Add("@in_humanoid", character.Stand?.Humanoid);
        UpdateParams.Add("@in_appareances", character.Appareances);

        await db.ExecuteAsync("update_character", UpdateParams, commandType: CommandType.StoredProcedure);
    }
    public async Task Delete(int id)
    {
        var db = _db.DbConnection();
        string query =
        @"
            DELETE FROM STANDS WHERE char_id = @char_id;
            DELETE FROM appareances WHERE char_id = @char_id;
            DELETE FROM characters WHERE char_id = @char_id;
        ";
        await db.ExecuteAsync(query, new { char_id = id });
    }

}