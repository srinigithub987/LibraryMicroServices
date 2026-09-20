using System.Text;

using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

using Ocelot.DependencyInjection;
using Ocelot.Middleware;


var builder = WebApplication.CreateBuilder(args);


// =====================================================
// Ocelot Configuration
// =====================================================

builder.Configuration.AddJsonFile(
    "ocelot.json",
    optional: false,
    reloadOnChange: true
);


// =====================================================
// JWT Configuration
// =====================================================

var jwtSecretKey =
    builder.Configuration["Jwt:SecretKey"]
    ?? throw new InvalidOperationException(
        "JWT SecretKey is not configured.");

var jwtIssuer =
    builder.Configuration["Jwt:Issuer"]
    ?? throw new InvalidOperationException(
        "JWT Issuer is not configured.");

var jwtAudience =
    builder.Configuration["Jwt:Audience"]
    ?? throw new InvalidOperationException(
        "JWT Audience is not configured.");


builder.Services
    .AddAuthentication(
        JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(
        JwtBearerDefaults.AuthenticationScheme,
        options =>
        {
            options.RequireHttpsMetadata = false;

            options.TokenValidationParameters =
                new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,

                    IssuerSigningKey =
                        new SymmetricSecurityKey(
                            Encoding.UTF8.GetBytes(
                                jwtSecretKey)
                        ),

                    ValidateIssuer = true,

                    ValidIssuer = jwtIssuer,

                    ValidateAudience = true,

                    ValidAudience = jwtAudience,

                    ValidateLifetime = true,

                    ClockSkew =
                        TimeSpan.FromSeconds(30)
                };
        }
    );


builder.Services.AddAuthorization();

builder.Services.AddOcelot(builder.Configuration);


var app = builder.Build();


// =====================================================
// Middleware
// =====================================================

app.UseAuthentication();

app.UseAuthorization();

await app.UseOcelot();

app.Run();