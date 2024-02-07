
using Microsoft.EntityFrameworkCore;
using TP2part1.Controllers;
using TP2part1.Models.EntityFramework;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddRazorPages();

builder.Services.AddDbContext<Tp2part1Context>(options =>
  options.UseNpgsql(builder.Configuration.GetConnectionString("TP2Part1")));

var app = builder.Build();

app.UseCors(policy =>
    policy.WithOrigins("https://localhost:7010/")
    .AllowAnyMethod()
    .AllowAnyHeader()
    .AllowCredentials()     
    );

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
