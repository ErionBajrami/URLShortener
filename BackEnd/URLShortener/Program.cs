using Microsoft.EntityFrameworkCore;
using System;
// using URLShortener.Data;
using URLShortener.Database;
using URLShortener.Service.Url;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IUrlService, UrlService>();
builder.Services.AddScoped<IUrlValidationService, UrlValidationService>();

// Add controllers and other MVC-related services
builder.Services.AddControllers();

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

app.UseCors(x => x.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());

app.UseAuthorization();

app.MapControllers();

//Seed the database
// DbInitializer.SeedDefaultData(app);

app.Run();

