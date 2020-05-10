using System.Threading.Tasks;

namespace Repository
{
    public interface IPhoneBookRepository
    {
        // Creates database table.
        Task CreateRepositoryAsync();
        
        // Update or insert phone number for specified name.
        Task StoreAsync(string name, string phoneNumber);

        // Return phone number for specified name.
        Task<string?> GetPhoneNumberAsync(string name);
    }
}