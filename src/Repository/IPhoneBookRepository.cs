using System.Threading.Tasks;

namespace Repository
{
    public interface IPhoneBookRepository
    {
        // Creates database table.
        Task CreateRepositoryAsync();
        
        // Updates or inserts phone number for the specified name.
        Task StorePhoneNumberAsync(string name, string phoneNumber);

        // Gets phone number for the specified name.
        Task<string?> GetPhoneNumberAsync(string name);
    }
}