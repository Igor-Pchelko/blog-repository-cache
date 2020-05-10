using System.Threading.Tasks;
using Xunit;

namespace Repository
{
    public class PhoneBookRepositoryTest
    {
        [Fact]
        public async Task Should_get_stored_phone_number()
        {
            // Arrange
            var repository = new PhoneBookRepository();
            var name = "Luke";
            var phoneNumber = "111-222-333";
            
            // Act
            await repository.CreateRepositoryAsync();
            await repository.StoreAsync(name, phoneNumber);
            var resultPhoneNumber = await repository.GetPhoneNumberAsync(name);
            
            // Assert
            Assert.Equal(phoneNumber, resultPhoneNumber);
        }
    }
}
