using System.Data.SQLite;
using System.Threading.Tasks;
using Dapper;

namespace Repository
{
    public class Repository : IRepository
    {
        private const string TableName = "phonebook";
        
        private SQLiteConnection CreateConnection()
        {
            var connectionString = $"Data Source=repository.sqlite;Mode=Memory;Pooling=True";
            return new SQLiteConnection(connectionString);
        }

        public async Task CreateRepositoryAsync()
        {
            var sql = $@"
                CREATE TABLE IF NOT EXISTS {TableName} (
		                name NVARCHAR(255),
                        phoneNumber NVARCHAR(255),
		                PRIMARY KEY( [name] )
	                );
                ";

            await using var connection = CreateConnection();
            await connection.ExecuteAsync(sql);
        }

        public async Task StoreAsync(string name, string phoneNumber)
        {
            var sql = $@"
                INSERT INTO {TableName} (name, phoneNumber)
		            VALUES(@Name, @PhoneNumber)
                ON CONFLICT (name) DO UPDATE SET 
                    phoneNumber = excluded.phoneNumber;
                ";

            await using var connection = CreateConnection();
            await connection.ExecuteAsync(sql, new { Name = name, PhoneNumber = phoneNumber });
        }

        public async Task<string?> GetAsync(string name)
        {
            var sql = $@"
                SELECT phoneNumber FROM {TableName} 
                WHERE name = @Name;";

            await using var connection = CreateConnection();
            return await connection.QueryFirstOrDefaultAsync<string>(sql, new { Name = name });
        }
    }
}