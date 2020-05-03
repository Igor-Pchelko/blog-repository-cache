using System.Collections.Generic;

namespace Repository
{
    public interface ICache
    {
        Dictionary<string, string> Cache { get; }
    }
}