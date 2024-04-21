using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Text;

// using URLShortener.Data;
using URLShortener.Database;
using URLShortener.Service.Url;
using URLShortener.Service.User;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IUrlService, UrlService>();
builder.Services.AddScoped<IUrlValidationService, UrlValidationService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddAuthorization();
//builder.Services.AddAuthentication()
//    .AddJwtBearer();
//builder.Services.ConfigureOptions<JwtBearerOptions>();
//builder.Services.AddAuthentication(options =>
//{
//    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
//    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
//})
//.AddJwtBearer(options =>
//{
//    var configuration = builder.Configuration; // Accessing Configuration via builder
//    options.TokenValidationParameters = new TokenValidationParameters
//    {
//        ValidateIssuer = true,
//        ValidateAudience = true,
//        ValidateLifetime = true,
//        ValidateIssuerSigningKey = true,
//        ValidIssuer = configuration["Jwt:Issuer"],
//        ValidAudience = configuration["Jwt:Issuer"],
//        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]))
//    };
//});


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

app.UseAuthentication();

//app.UseAuthorization();

app.MapControllers();

//Seed the database
// DbInitializer.SeedDefaultData(app);

app.Run();

