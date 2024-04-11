using System.Runtime.CompilerServices;
using Jojo.Models;

namespace Jojo.Data;

public interface ICharacterData
{
    
    public Task<IEnumerable<Character>> Get(int? id);
    public Task Insert(Character character);
    public Task Update(Character character);
    public Task Delete(int id);
    

}