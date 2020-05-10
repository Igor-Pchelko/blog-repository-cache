using System.Collections.Generic;
using System.Threading.Tasks;
using System.Transactions;

namespace Repository
{
    public class CachedPhoneBookRepository : IPhoneBookRepository, ICache
    {
        public Dictionary<string, string> Cache { get; } = new Dictionary<string, string>();
        private readonly IPhoneBookRepository _phoneBookRepository;

        public CachedPhoneBookRepository(IPhoneBookRepository phoneBookRepository)
        {
            _phoneBookRepository = phoneBookRepository;
        }

        public Task CreateRepositoryAsync() => _phoneBookRepository.CreateRepositoryAsync();

        public async Task StorePhoneNumberAsync(string name, string phoneNumber)
        {
            await _phoneBookRepository.StorePhoneNumberAsync(name, phoneNumber);

            if (Transaction.Current != null)
            {
                Transaction.Current.EnlistVolatile(
                    new EnlistResource(() => StoreInternal(name, phoneNumber)), 
                    EnlistmentOptions.None);
            }
            else
            {
                StoreInternal(name, phoneNumber);
            }
        }

        private void StoreInternal(string name, string phoneNumber)
        {
            Cache[name] = phoneNumber;
        }

        public async Task<string?> GetPhoneNumberAsync(string name)
        {
            if (Cache.TryGetValue(name, out var phoneNumber))
            {
                return phoneNumber;
            }

            phoneNumber = await _phoneBookRepository.GetPhoneNumberAsync(name);

            if (phoneNumber != null)
            {
                Cache[name] = phoneNumber;
            }
            
            return phoneNumber;
        }
    }
}