using System.Collections.Generic;
using System.Threading.Tasks;

namespace Repository
{
    public class CachedRepository : IRepository, ICache
    {
        public Dictionary<string, string> Cache { get; } = new Dictionary<string, string>();
        private readonly IRepository _repository;

        public CachedRepository(IRepository repository)
        {
            _repository = repository;
        }

        public Task CreateRepositoryAsync() => _repository.CreateRepositoryAsync();

        public async Task StoreAsync(string name, string phoneNumber)
        {
            await _repository.StoreAsync(name, phoneNumber);
            Cache[name] = phoneNumber;
        }

        public async Task<string?> GetAsync(string name)
        {
            if (Cache.TryGetValue(name, out var phoneNumber))
            {
                return phoneNumber;
            }

            phoneNumber = await _repository.GetAsync(name);

            if (phoneNumber != null)
            {
                Cache[name] = phoneNumber;
            }
            
            return phoneNumber;
        }
    }
}