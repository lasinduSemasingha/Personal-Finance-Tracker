using PersonalFinanceTracker.Database;
using PersonalFinanceTracker.Endpoints;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();

builder.Services.AddSingleton<DatabaseContext>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.MapTransactionEndpoint();

app.UseHttpsRedirection();

app.Run();
