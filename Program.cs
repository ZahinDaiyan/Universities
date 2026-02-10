using Microsoft.EntityFrameworkCore;
using UniversityApi.Data;
using UnversityApi.Services;

var builder = WebApplication.CreateBuilder(args);

//Controllers
builder.Services.AddControllers();

// Swagger 
builder.Services.AddEndPointApiExplorer();
builder.Services.AddSwaggerGen();

// DbContext
builder.Services.AppDbContext<AppDbContext> (options =>
		options.UseSqlite(builder.Configuration.GetConnentionString("DefaultConnection"));

var app = builder.Build();

if ( app.Enviourment.IsDevlopment()){
	app.UseSwagger();
	app.UserSwaggerUI();
	}

app.MapGet("/", () => "Hello World!");
app.MapControllers();

app.Run();
