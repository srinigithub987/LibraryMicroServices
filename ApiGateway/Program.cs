using Ocelot.DependencyInjection;
using Ocelot.Middleware;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration
    .AddJsonFile(
        "ocelot.json",
        optional: false,
        reloadOnChange: true);

//builder.Services.AddCors(options =>
//{
//    options.AddPolicy("AngularPolicy", policy =>
//    {
//        policy
//           //.WithOrigins("http://localhost:4200")
//            .AllowAnyHeader()
//            .AllowAnyMethod()
//            .AllowCredentials();
//    });
//});

builder.Services.AddOcelot(
    builder.Configuration);

var app = builder.Build();

await app.UseOcelot();

app.Run();