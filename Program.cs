using Microsoft.OpenApi.Models;
using portfolioApi.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Security.Cryptography;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

// Cors
//builder.Services.AddCors(options =>
//{
//    options.AddPolicy("CorsPolicy",
//       builder => builder
//        .WithOrigins("http://localhost:3000", "http://localhost:3001") 
//        .AllowAnyMethod()
//        .AllowAnyHeader());
//});

// DbContext and Identity
builder.Services.AddDbContext<ProfileContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("LocalDb")));

// Swagger API
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" });
});

var app = builder.Build();

// Cors
app.UseCors(b => b.WithOrigins("http://localhost:3000", "http://localhost:3001").AllowAnyMethod().AllowAnyHeader().AllowCredentials().WithHeaders("authorization"));

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
    });
}

app.UseHttpsRedirection();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

//app.UseCors("CorsPolicy");

app.MapControllers();

app.Run();
