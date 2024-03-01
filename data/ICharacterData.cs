using Jojo.Models;

namespace Jojo.Data;

public interface ICharacterData
{
    public Task<IEnumerable<Character>> GetAll();
}