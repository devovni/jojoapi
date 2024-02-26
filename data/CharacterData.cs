using System.Data.Common;
using System.Threading.Tasks;
using Jojo.Models;

namespace Jojo.Data;

public class CharacterData(IDataContext db) : ICharacterData
{
    private readonly IDataContext dataContext = db;

    public Task<IEnumerable<Character>> GetAll() => dataContext.LoadData<Character, dynamic>("get_all", new{});
}