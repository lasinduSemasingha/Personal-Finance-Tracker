using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using PersonalFinanceTracker.Database;
using PersonalFinanceTracker.Endpoints;
using PersonalFinanceTracker.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();

builder.Services.AddSingleton<DatabaseContext>();
builder.Services.AddScoped<ITransactionService, TransactionMain>();
builder.Services.AddScoped<IUserService, UserMain>();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllOrigins",
        policy =>
        {
            policy.AllowAnyOrigin()
                  .AllowAnyMethod()
                  .AllowAnyHeader();
        });
});

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = "http://localhost:5286",
            ValidAudience = "http://localhost:5286",
            IssuerSigningKey = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes("YourVeryLongSecretKeyHere12345678901234567890123456789012"))
        };
    });

var app = builder.Build();

app.UseCors("AllowAllOrigins");

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.MapTransactionEndpoint();
app.MapUserEndpoint();

app.UseAuthentication();

app.UseHttpsRedirection();

app.Run();
