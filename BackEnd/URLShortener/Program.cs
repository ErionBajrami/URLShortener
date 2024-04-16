using Microsoft.EntityFrameworkCore;
using System;
// using URLShortener.Data;
using URLShortener.Database;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Configure the DB context
//builder.Services.AddDbContext<UrlShortenerDbContext>(options =>
//{
//    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
//});

builder.Services.AddDbContext<UrlShortenerDbContext>(options => options
    .UseNpgsql("Host=localhost;Database=URLSHORTENER;Username=postgres;Password=postgres"));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

//Seed the database
// DbInitializer.SeedDefaultData(app);

app.Run();

