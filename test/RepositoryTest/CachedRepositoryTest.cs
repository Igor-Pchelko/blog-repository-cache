using System.Threading.Tasks;
using Xunit;

namespace Repository
{
    public class CachedRepositoryTest
    {
        [Fact]
        public async Task Should_get_stored_phone_number()
        {
            // Arrange
            var repository = new Repository();
            var cachedRepository = new CachedRepository(repository);
            var name = "Dave";
            var phoneNumber = "222-333-111";
            
            // Act
            await cachedRepository.CreateRepositoryAsync();
            await cachedRepository.StoreAsync(name, phoneNumber);
            var resultPhoneNumber = await cachedRepository.GetAsync(name);
            
            // Assert
            Assert.Equal(phoneNumber, resultPhoneNumber);
        }
    }
}