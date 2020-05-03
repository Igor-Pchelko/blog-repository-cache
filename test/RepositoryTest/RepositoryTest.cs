using System.Threading.Tasks;
using Xunit;

namespace Repository
{
    public class RepositoryTest
    {
        [Fact]
        public async Task Should_get_stored_phone_number()
        {
            // Arrange
            var repository = new Repository();
            var name = "Luke";
            var phoneNumber = "111-222-333";
            
            // Act
            await repository.CreateRepositoryAsync();
            await repository.StoreAsync(name, phoneNumber);
            var resultPhoneNumber = await repository.GetAsync(name);
            
            // Assert
            Assert.Equal(phoneNumber, resultPhoneNumber);
        }
    }
}
