using Microsoft.OpenApi.Models;
using portfolioApi.Context;
using Microsoft.EntityFrameworkCore;
using portfolioApi.Components.Middleware;
using dotenv.net;
using Microsoft.Extensions.FileProviders;
using System.Net.Http.Headers;
using portfolioApi.Services;
using Microsoft.Extensions.Logging;
using Serilog;
using Serilog.Events;

// Load environment variables from .env file
DotEnv.Load();

// Get connection string from environment variables
var connectionString = Environment.GetEnvironmentVariable("CONNECTION_STRING");
var smtpUsername = Environment.GetEnvironmentVariable("SMTP_USERNAME");
var smtpPassword = Environment.GetEnvironmentVariable("SMTP_PASSWORD");

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers().AddNewtonsoftJson(options =>
        options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);
builder.Services.Configure<SmtpSettings>(options =>
{
    options.Server = "smtp-mail.outlook.com";
    options.Port = 587;
    options.SenderName = "Lirije Shabani";
    options.SenderEmail = "Lirije11@hotmail.com";
    options.Username = smtpUsername;
    options.Password = smtpPassword;
});
builder.Services.AddSingleton<EmailService>(); builder.Services.AddSingleton<EmailService>();

// DbContext and Identity
builder.Services.AddDbContext<ProfileContext>(options =>
    options.UseSqlServer(connectionString));

// Configure logging
builder.Services.AddLogging(logging =>
{
    logging.ClearProviders();
    logging.AddConsole();
    logging.SetMinimumLevel(LogLevel.Information);
});

// Swagger API
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" });

});
//builder.Services.AddSwaggerGen(c =>
//{
//    c.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" });
//    c.AddSecurityDefinition("API_KEY", new OpenApiSecurityScheme
//    {
//        Description = "API Key needed to access the endpoints. ApiKey: Your_API_Key",
//        In = ParameterLocation.Header,
//        Name = "API_KEY",
//        Type = SecuritySchemeType.ApiKey,
//        Scheme = "ApiKeyScheme"
//    });

//    c.AddSecurityRequirement(new OpenApiSecurityRequirement
//    {
//        {
//            new OpenApiSecurityScheme
//            {
//                Reference = new OpenApiReference
//                {
//                    Type = ReferenceType.SecurityScheme,
//                    Id = "API_KEY"
//                },
//                Name = "API_KEY",
//                In = ParameterLocation.Header,
//            },
//            new List<string>()
//        }
//    });
//});

var app = builder.Build();

// Serve static files from a custom directory
var staticFileDirectory = @"h:\root\home\lirijes-001\www\portfolio\images";

if (!Directory.Exists(staticFileDirectory))
{
    Console.WriteLine($"Directory does NOT exist: {staticFileDirectory}");
    staticFileDirectory = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images");
    Console.WriteLine($"Falling back to: {staticFileDirectory}");
}

var fileProvider = new PhysicalFileProvider(staticFileDirectory);
var requestPath = "/images";

app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(staticFileDirectory),
    RequestPath = requestPath
});

// Cors
app.UseCors(b => b.AllowAnyOrigin()
    .AllowAnyMethod()
    .AllowAnyHeader());

// Use Api Key Middleware
app.UseMiddleware<ApiKeyMiddleware>();

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();