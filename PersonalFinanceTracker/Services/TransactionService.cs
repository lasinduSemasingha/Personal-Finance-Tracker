using PersonalFinanceTracker.DTO;
using PersonalFinanceTracker.Entities;

namespace PersonalFinanceTracker.Services
{
    public interface ITransactionService
    {
        Task<IEnumerable<Transaction>> GetTransactionsAsync();
        Task<Transaction?> GetTransactionByIdAsync(int id);
        Task<bool> CreateTransactionAsync(CreateTransactionRequest request);
        Task<bool> UpdateTransactionAsync(int id, UpdateTransactionRequest request);
        Task<bool> DeleteTransactionAsync(int id);
    }

}
