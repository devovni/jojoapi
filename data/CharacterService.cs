using Jojo.Models;

namespace Jojo.Data;

public static class CharacterService
{
   
    public static async Task<IResult> GetCharById(ICharacterData data, int? id)
    {
        try
        {
            IEnumerable<Character> result = await data.Get(id);
            if (result.Any())
            {
                return Results.Ok(result);
            }
            return Results.NotFound(result);
        }
        catch (Exception ex)
        {
            return Results.Problem(ex.Message);
        }
    }

    public static async Task<IResult> InsertCharacter(ICharacterData data, Character character)
    {
        try
        {
            await data.Insert(character);
            return Results.Ok();
        }
        catch (Exception ex)
        {
            return Results.Problem(ex.Message);
        }

    }
    
    public static async Task<IResult> UpdateCharacter(ICharacterData data, Character character)
    {
        try
        {
            await data.Update(character);
            return Results.Ok();
        }
        catch (Exception ex)
        {
            return Results.Problem(ex.Message);
        }

    }

    
    public static async Task<IResult> DeleteCharacter(ICharacterData data, int id)
    {
        try
        {
            await data.Delete(id);
            return Results.Ok();
        }
        catch (Exception ex)
        {
            return Results.Problem(ex.Message);
        }

    }
}