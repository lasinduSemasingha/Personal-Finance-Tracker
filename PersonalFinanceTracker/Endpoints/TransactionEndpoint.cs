using Dapper;
using Microsoft.EntityFrameworkCore;
using Npgsql;
using PersonalFinanceTracker.DTO;
using PersonalFinanceTracker.Entities;
using PersonalFinanceTracker.Database;

namespace PersonalFinanceTracker.Endpoints
{
    public static class TransactionEndpoint
    {
        public static void MapTransactionEndpoint(this IEndpointRouteBuilder app)
        {
            app.MapGet("/transaction", async (DatabaseContext context) =>
            {
                const string query = "SELECT id, amount, date, description, categoryid, userid FROM \"Personal_Finance\".transaction";

                using var connection = context.Create();

                try
                {
                    var transactions = await connection.QueryAsync<Transaction>(query);
                    return Results.Ok(transactions);
                }
                catch (NpgsqlException ex)
                {
                    return Results.Json(new { error = $"Database error: {ex.Message}" }, statusCode: 500);
                }
                catch (Exception ex)
                {
                    return Results.Json(new { error = $"Unexpected error: {ex.Message}" }, statusCode: 500);
                }
            });

            app.MapPost("/transaction", async (CreateTransactionRequest request, DatabaseContext context) =>
            {
                using var connection = context.Create();

                const string query = "INSERT INTO \"Personal_Finance\".transaction (amount, date, description, categoryid, userid) " +
                                        "VALUES (@amount, @date, @description, @categoryid, @userid)";

                try
                {
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
                    return Results.Json(new { error = $"Database error: {ex.Message}" }, statusCode: 500);
                }
                catch (Exception ex)
                {
                    return Results.Json(new { error = $"Unexpected error: {ex.Message}" }, statusCode: 500);
                }
            });
            app.MapGet("/transaction/{id}", async (DatabaseContext context, int id) =>
            {
                using var connection = context.Create();

                const string query = "SELECT id, amount, date, description, categoryid, userid FROM \"Personal_Finance\".transaction WHERE id = @id";

                try
                {
                    var transactions = await connection.QuerySingleOrDefaultAsync<Transaction>(query,
                        new
                        {
                            id = id
                        });
                    return transactions != null ? Results.Ok(transactions) : Results.NotFound($"Transaction with ID {id} not found.");
                }
                catch (NpgsqlException ex)
                {
                    return Results.Json(new { error = $"Database error: {ex.Message}" }, statusCode: 500);
                }
                catch (Exception ex)
                {
                    return Results.Json(new { error = $"Unexpected error: {ex.Message}" }, statusCode: 500);
                }
            });
            app.MapPut("/transaction/{id}", async (UpdateTransactionRequest request, DatabaseContext context, int id) =>
            {
                using var connection = context.Create();

                const string query = "UPDATE \"Personal_Finance\".transaction SET amount = @amount, date = @date, description = @description, categoryid = @categoryid WHERE id = @id";

                try
                {
                    await connection.ExecuteAsync(query, new
                    {
                        id = request.id,
                        amount = request.Amount,
                        date = DateTime.Now,
                        description = request.Description,
                        categoryid = request.CategoryId,
                        userid = request.UserId
                    });
                    return Results.Json(new { message = "Transaction updated successfully" });
                }
                catch (NpgsqlException ex)
                {
                    return Results.Json(new { error = $"Database error: {ex.Message}" }, statusCode: 500);
                }
                catch (Exception ex)
                {
                    return Results.Json(new { error = $"Unexpected error: {ex.Message}" }, statusCode: 500);
                }
            });
            app.MapDelete("/transaction/{id}", async (DatabaseContext context, int id) =>
            {
                using var connection = context.Create();

                const string query = "DELETE FROM \"Personal_Finance\".transaction WHERE id = @id";

                try
                {
                    await connection.ExecuteAsync(query, new
                    {
                        id = id
                    });
                    return Results.Json(new { message = "Transaction Deleted successfully" });
                }
                catch (NpgsqlException ex)
                {
                    return Results.Json(new { error = $"Database error: {ex.Message}" }, statusCode: 500);
                }
                catch (Exception ex)
                {
                    return Results.Json(new { error = $"Unexpected error: {ex.Message}" }, statusCode: 500);
                }
            });
        }
    }
}
