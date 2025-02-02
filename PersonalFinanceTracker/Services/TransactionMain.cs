using Dapper;
using Npgsql;
using PersonalFinanceTracker.DTO;
using PersonalFinanceTracker.Entities;
using PersonalFinanceTracker.Database;

namespace PersonalFinanceTracker.Services
{
    public class TransactionMain : ITransactionService
    {
        private readonly DatabaseContext _context;

        public TransactionMain(DatabaseContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Transaction>> GetTransactionsAsync()
        {
            using var connection = _context.Create();
            const string query = "SELECT id, amount, date, description, categoryid, userid FROM \"Personal_Finance\".transaction";
            return await connection.QueryAsync<Transaction>(query);
        }

        public async Task<Transaction?> GetTransactionByIdAsync(int id)
        {
            using var connection = _context.Create();
            const string query = "SELECT id, amount, date, description, categoryid, userid FROM \"Personal_Finance\".transaction WHERE id = @id";
            return await connection.QuerySingleOrDefaultAsync<Transaction>(query, new { id });
        }

        public async Task<bool> CreateTransactionAsync(CreateTransactionRequest request)
        {
            using var connection = _context.Create();
            const string query = "INSERT INTO \"Personal_Finance\".transaction (amount, date, description, categoryid, userid) VALUES (@amount, @date, @description, @categoryid, @userid)";
            int rowsAffected = await connection.ExecuteAsync(query, new
            {
                amount = request.Amount,
                date = DateTime.Now,
                description = request.Description,
                categoryid = request.CategoryId,
                userid = request.UserId
            });
            return rowsAffected > 0;
        }

        public async Task<bool> UpdateTransactionAsync(int id, UpdateTransactionRequest request)
        {
            using var connection = _context.Create();
            const string query = "UPDATE \"Personal_Finance\".transaction SET amount = @amount, date = @date, description = @description, categoryid = @categoryid WHERE id = @id";
            int rowsAffected = await connection.ExecuteAsync(query, new
            {
                id,
                amount = request.Amount,
                date = DateTime.Now,
                description = request.Description,
                categoryid = request.CategoryId
            });
            return rowsAffected > 0;
        }

        public async Task<bool> DeleteTransactionAsync(int id)
        {
            using var connection = _context.Create();
            const string query = "DELETE FROM \"Personal_Finance\".transaction WHERE id = @id";
            int rowsAffected = await connection.ExecuteAsync(query, new { id });
            return rowsAffected > 0;
        }
    }
}
