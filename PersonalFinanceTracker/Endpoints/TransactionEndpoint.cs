using Dapper;
using Npgsql;
using PersonalFinanceTracker.DTO;
using PersonalFinanceTracker.Entities;

namespace PersonalFinanceTracker.Endpoints
{
    public static class TransactionEndpoint
    {
        public static void MapTransactionEndpoint(this IEndpointRouteBuilder app)
        {
            app.MapGet("/transactions", async (IConfiguration configuration) =>
            {
                string? connectionString = configuration.GetConnectionString("DefaultConnection");

                const string query = "SELECT id, amount, date, description, categoryid, userid FROM \"Personal_Finance\".transaction";

                try
                {
                    using var connection = new NpgsqlConnection(connectionString);
                    var transactions = await connection.QueryAsync<Transaction>(query);
                    return Results.Ok(transactions);
                }
                catch (NpgsqlException ex)
                {
                    // Log or return detailed error message
                    return Results.Json(new { error = $"Database error: {ex.Message}" }, statusCode: 500);
                }
                catch (Exception ex)
                {
                    // Catch any other exceptions
                    return Results.Json(new { error = $"Unexpected error: {ex.Message}" }, statusCode: 500);
                }
            });
            app.MapPost("/transactions", async (CreateTransactionRequest request, IConfiguration configuration) =>
            {
                string? connectionString = configuration.GetConnectionString("DefaultConnection");

                const string query = "INSERT INTO \"Personal_Finance\".transaction (amount, date, description, categoryid, userid) " +
                                        "VALUES (@amount, @date, @description, @categoryid, @userid)";

                try
                {
                    using var connection = new NpgsqlConnection(connectionString);
                    
                    await connection.ExecuteAsync(query, new
                    {
                        amount = request.Amount,
                        date = DateTime.Now,
                        description = request.Description,
                        categoryid = request.CategoryId,
                        userid = request.UserId
                    });
                    return Results.Json(new { message = "Transaction created successfully"});
                }
                catch (NpgsqlException ex)
                {
                    // Log or return detailed error message
                    return Results.Json(new { error = $"Database error: {ex.Message}" }, statusCode: 500);
                }
                catch (Exception ex)
                {
                    // Catch any other exceptions
                    return Results.Json(new { error = $"Unexpected error: {ex.Message}" }, statusCode: 500);
                }
            });
        }
    }
}
