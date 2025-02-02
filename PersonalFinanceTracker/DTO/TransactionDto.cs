namespace PersonalFinanceTracker.DTO
{
    public sealed record CreateTransactionRequest ( decimal Amount, DateTime Date, string Description, int CategoryId, int UserId );
    public sealed record UpdateTransactionRequest (int id, decimal Amount, DateTime Date, string Description, int CategoryId, int UserId );
}
