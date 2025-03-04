namespace PersonalFinanceTracker.DTO
{
    public sealed record CreateTransactionRequest ( decimal Amount, DateTime Date, string Description, int CategoryId, int UserId );
    public sealed record UpdateTransactionRequest (int id, decimal Amount, DateTime Date, string Description, int CategoryId, int UserId );
    public sealed record AuthenticationCredentials (string username, string password );
    public sealed record CreateUser (string first_name, string last_name, string email, string username, string password, DateTime createdAt, DateTime updatedAt);
}
