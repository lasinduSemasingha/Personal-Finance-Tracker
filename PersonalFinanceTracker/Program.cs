using PersonalFinanceTracker.Database;
using PersonalFinanceTracker.Endpoints;
using PersonalFinanceTracker.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();

builder.Services.AddSingleton<DatabaseContext>();
builder.Services.AddScoped<ITransactionService, TransactionMain>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.MapTransactionEndpoint();

app.UseHttpsRedirection();

app.Run();
