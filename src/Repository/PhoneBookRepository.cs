using System.Data.SqlClient;
using System.Threading.Tasks;
using Dapper;

namespace Repository
{
    public class PhoneBookRepository : IPhoneBookRepository
    {
        private const string TableName = "phonebook";

        private SqlConnection CreateConnection()
        {
            const string connectionString = "Server=tcp:localhost,1433;Initial Catalog=phonebookdb;Persist Security Info=False;User ID=sa;Password=1234567_A;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=True;Connection Timeout=30;";
            return new SqlConnection(connectionString);
        }

        public async Task CreateRepositoryAsync()
        {
            var sql = $@"
            IF object_id('{TableName}', 'U') is null
	            CREATE TABLE {TableName} (
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
                MERGE INTO [{TableName}] WITH(HOLDLOCK) AS TARGET 
                USING (
                  VALUES
                    (@Name, @PhoneNumber)
                ) AS SOURCE (
                   [Name], [PhoneNumber]
                ) 
                ON TARGET.[Name] = SOURCE.[Name] AND TARGET.[PhoneNumber] = SOURCE.[PhoneNumber]
                WHEN MATCHED THEN UPDATE SET
                  TARGET.[PhoneNumber] = SOURCE.[PhoneNumber]
                WHEN NOT MATCHED BY TARGET THEN INSERT (
                  [Name], [PhoneNumber]
                ) VALUES (
                  SOURCE.[Name], SOURCE.[PhoneNumber]
                );
            ";
            
            await using var connection = CreateConnection();
            await connection.ExecuteAsync(sql, new { Name = name, PhoneNumber = phoneNumber });
        }

        public async Task<string?> GetPhoneNumberAsync(string name)
        {
            var sql = $@"
                SELECT phoneNumber FROM {TableName} 
                WHERE name = @Name;";

            await using var connection = CreateConnection();
            return await connection.QueryFirstOrDefaultAsync<string>(sql, new { Name = name });
        }
    }
}