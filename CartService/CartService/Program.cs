using CartService.Data;
using CartService.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var jwtSecretKey = builder.Configuration["Jwt:SecretKey"] //"MyLibraryManagementSecretKey123456789!" 
    ?? throw new InvalidOperationException(
        "JWT SecretKey is not configured.");

var jwtIssuer =  builder.Configuration["Jwt:Issuer"] //"LibraryManagement.CustomerService" /
    ?? throw new InvalidOperationException(
        "JWT Issuer is not configured.");

var jwtAudience = builder.Configuration["Jwt:Audience"] //"LibraryManagement.Api" 
    ?? throw new InvalidOperationException(
        "JWT Audience is not configured.");


Console.WriteLine($"JWT key loaded: {!string.IsNullOrWhiteSpace(jwtSecretKey)}");
Console.WriteLine($"JWT key length: {jwtSecretKey.Length}");
Console.WriteLine($"JWT issuer: {jwtIssuer}");
Console.WriteLine($"JWT audience: {jwtAudience}");


builder.Services
    .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.RequireHttpsMetadata = false;

        options.TokenValidationParameters =
            new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,

                IssuerSigningKey =
                    new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes(jwtSecretKey)
                    ),

                ValidAlgorithms = new[]
                {
                    SecurityAlgorithms.HmacSha256
                },

                ValidateIssuer = true,
                ValidIssuer = jwtIssuer,

                ValidateAudience = true,
                ValidAudience = jwtAudience,

                ValidateLifetime = true,

                ClockSkew = TimeSpan.FromSeconds(30),

                NameClaimType = "email"
            };

        options.Events = new JwtBearerEvents
        {
            OnAuthenticationFailed = context =>
            {
                Console.WriteLine("===== JWT AUTH FAILED =====");
                Console.WriteLine(
                    $"Type: {context.Exception.GetType().FullName}");
                Console.WriteLine(
                    $"Message: {context.Exception.Message}");
                Console.WriteLine(
                    context.Exception.ToString());

                return Task.CompletedTask;
            },

            OnTokenValidated = context =>
            {
                Console.WriteLine("===== JWT VALIDATED =====");

                foreach (var claim in context.Principal!.Claims)
                {
                    Console.WriteLine(
                        $"{claim.Type} = {claim.Value}");
                }

                return Task.CompletedTask;
            }
        };
    });

builder.Services.AddAuthorization();

builder.Services.AddControllers();
builder.Services.AddDbContext<CartDbContext>(
    options =>
        options.UseNpgsql(
            builder.Configuration
                .GetConnectionString("CartDb")));

builder.Services.AddScoped<ICartService,
    CartService.Services.CartService>();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
