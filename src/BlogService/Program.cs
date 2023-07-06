using Infrastructure.Data;
using Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Hosting;
using System.Reflection;
using Application;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services
    .AddControllers()
    // Add Json Handeler
    .AddNewtonsoftJson(options =>
    {
        options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
        options.SerializerSettings.NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore;
    });

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


// Get connection string 
string? DB_CONNECTION_STRING
    = Environment.GetEnvironmentVariable("DB_CONNECTION_STRING")
    ?? builder.Configuration.GetConnectionString("DefaultConnection");

// Get assambly name
var AssamblyName = typeof(Program).Assembly.GetName().Name;

// Add Infrastructure Layer
builder.Services.AddInfrastructure(option =>
{
    option.AddDbConnectionString(DB_CONNECTION_STRING)
          .AddDbMigrationAssembly(AssamblyName);
});


// Add CORS
builder.Services.AddCors();
builder.Services.Configure<RouteOptions>(options => options.LowercaseUrls = true);

// Add Application Layer Config
builder.Services.AddApplicationServices();



// BUILD APP
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    Console.WriteLine(DB_CONNECTION_STRING);
    app.UseCors(op =>
    {
        op.AllowAnyOrigin();
        op.AllowAnyMethod();
        op.AllowAnyHeader();
    });
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
