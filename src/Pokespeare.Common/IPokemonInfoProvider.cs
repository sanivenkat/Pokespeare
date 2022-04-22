using System;
using System.Threading.Tasks;

namespace Pokespeare.Common
{
    public interface IPokemonInfoProvider
    {
        Task<string> GetDescriptionAsync(string name);
    }
}
