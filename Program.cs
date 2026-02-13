using Microsoft.EntityFrameworkCore;
using UniversityApi.Data;
using UniversityApi.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services
builder.Services.AddControllers();

// Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// DbContext (SQLite)
builder.Services.AddDbContext<AppDbContext>(options =>
	options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

var app = builder.Build();

// Enable Swagger for all environments (good for local testing)
app.UseSwagger();
app.UseSwaggerUI(c =>
{
	c.SwaggerEndpoint("/swagger/v1/swagger.json", "University API V1");
	c.RoutePrefix = "swagger"; // Swagger will be served at /swagger
});

// Default route
app.MapGet("/", () => "Hello World!");

// Map controllers
app.MapControllers();

app.Run();
