using System.Data;
using Npgsql;

namespace PersonalFinanceTracker.Database
{
    public sealed class DatabaseContext
    {
        private readonly string _connectionString;

        public DatabaseContext(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection")!;
        }

        public IDbConnection Create() => new NpgsqlConnection(_connectionString);
    }
}
