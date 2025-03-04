using PersonalFinanceTracker.DTO;
using PersonalFinanceTracker.Entities;
using PersonalFinanceTracker.Services;

namespace PersonalFinanceTracker.Endpoints
{
    public static class TransactionEndpoint
    {
        public static void MapTransactionEndpoint(this IEndpointRouteBuilder app)
        {
            app.MapGet("/transaction", async (ITransactionService service) =>
            {
                var transactions = await service.GetTransactionsAsync();
                return Results.Ok(transactions);
            });

            app.MapPost("/transaction", async (CreateTransactionRequest request, ITransactionService service) =>
            {
                bool created = await service.CreateTransactionAsync(request);
                return created
                    ? Results.Ok(new { message = "Transaction created successfully" })
                    : Results.BadRequest(new { error = "Failed to create transaction" });
            });

            app.MapGet("/transaction/{id}", async (int id, ITransactionService service) =>
            {
                var transaction = await service.GetTransactionByIdAsync(id);
                return transaction != null
                    ? Results.Ok(transaction)
                    : Results.NotFound($"Transaction with ID {id} not found.");
            });

            app.MapPut("/transaction/{id}", async (int id, UpdateTransactionRequest request, ITransactionService service) =>
            {
                bool updated = await service.UpdateTransactionAsync(id, request);
                return updated
                    ? Results.Ok(new { message = "Transaction updated successfully" })
                    : Results.NotFound(new { error = $"Transaction with ID {id} not found." });
            });

            app.MapDelete("/transaction/{id}", async (int id, ITransactionService service) =>
            {
                bool deleted = await service.DeleteTransactionAsync(id);
                return deleted
                    ? Results.Ok(new { message = "Transaction deleted successfully" })
                    : Results.NotFound(new { error = $"Transaction with ID {id} not found." });
            });
        }
    }
}
