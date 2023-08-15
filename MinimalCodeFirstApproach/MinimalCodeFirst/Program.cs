using Microsoft.EntityFrameworkCore;
using MiniDB0731.EndPoints;
using MinimalCodeFirst.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Load the configuration from appsettings.json or other configuration files
var configuration = new ConfigurationBuilder()
    .SetBasePath(builder.Environment.ContentRootPath)
    .AddJsonFile("appsettings.json") // Add more files if needed
    .Build();

builder.Services.AddDbContext<CodeContext>(options =>
{
    options.UseNpgsql(configuration.GetConnectionString("MyDB")); // Use the correct connection string key from your appsettings.json
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.EmployeesEndPoint();

app.Run();