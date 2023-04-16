using BlogService.Data;
using BlogService.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add MyServide
builder.Services.AddMyService();

// Get connection string 
string? DB_CONNECTION_STRING
    = Environment.GetEnvironmentVariable("DB_CONNECTION_STRING")
    ?? builder.Configuration.GetConnectionString("DefaultConnection");

// Get assambly name
var AssamblyName = typeof(Program).Assembly.GetName().Name;

// Add DB context
builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseSqlServer(DB_CONNECTION_STRING, opt=>opt.MigrationsAssembly(AssamblyName));
});

// Add CORS
builder.Services.AddCors();

// BUILD APP
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    Console.WriteLine(DB_CONNECTION_STRING);
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
