using System.Threading.Tasks;

namespace Repository
{
    public interface IRepository
    {
        Task CreateRepositoryAsync();
        Task StoreAsync(string name, string phoneNumber);
        Task<string?> GetAsync(string name);
    }
}