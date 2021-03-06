using System.Threading.Tasks;
using System.Transactions;
using Xunit;

namespace Repository
{
    public class CachedPhoneBookRepositoryTest
    {
        [Fact]
        public async Task Should_get_stored_phone_number()
        {
            // Arrange
            var repository = new PhoneBookRepository();
            var cachedRepository = new CachedPhoneBookRepository(repository);
            var name = "Dave";
            var phoneNumber = "222-333-111";
            
            // Act
            await cachedRepository.CreateRepositoryAsync();
            await cachedRepository.StorePhoneNumberAsync(name, phoneNumber);
            var resultPhoneNumber = await cachedRepository.GetPhoneNumberAsync(name);
            
            // Assert
            Assert.Equal(phoneNumber, resultPhoneNumber);
        }

        [Fact]
        public async Task Should_update_cache_after_transaction_commit()
        {
            // Arrange
            var repository = new PhoneBookRepository();
            var cachedRepository = new CachedPhoneBookRepository(repository);
            var name1 = "Peter";
            var phoneNumber1 = "444-333-111";
            var name2 = "Mike";
            var phoneNumber2 = "555-333-111";
            
            // Act & Assert
            await cachedRepository.CreateRepositoryAsync();
            using (var transaction = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                await cachedRepository.StorePhoneNumberAsync(name1, phoneNumber1);
                await cachedRepository.StorePhoneNumberAsync(name2, phoneNumber2);

                Assert.False(cachedRepository.Cache.TryGetValue(name1, out _));
                Assert.False(cachedRepository.Cache.TryGetValue(name2, out _));
                transaction.Complete();
            }

            Assert.True(cachedRepository.Cache.TryGetValue(name1, out _));
            Assert.True(cachedRepository.Cache.TryGetValue(name2, out _));
        }
    }
}