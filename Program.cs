using Microsoft.OpenApi.Models;
using portfolioApi.Context;
using Microsoft.EntityFrameworkCore;
using portfolioApi.Components.Middleware;
using dotenv.net;
using dotenv.net.Utilities;

// Load environment variables from .env file
DotEnv.Load();

// Get connection string from environment variables
var connectionString = Environment.GetEnvironmentVariable("CONNECTION_STRING");

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers().AddNewtonsoftJson(options =>
        options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);

// DbContext and Identity
builder.Services.AddDbContext<ProfileContext>(options =>
    options.UseSqlServer(connectionString));


// Swagger API
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" });
    c.AddSecurityDefinition("ApiKey", new OpenApiSecurityScheme
    {
        Description = "API Key needed to access the endpoints. ApiKey: Your_API_Key",
        In = ParameterLocation.Header,
        Name = "ApiKey",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "ApiKeyScheme"
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "ApiKey"
                },
                Name = "ApiKey",
                In = ParameterLocation.Header,
            },
            new List<string>()
        }
    });
});

var app = builder.Build();

// Cors
app.UseCors(b => b.WithOrigins("http://localhost:3000", "http://localhost:3001")
    .AllowAnyMethod()
    .AllowAnyHeader()
    .AllowCredentials());

// Use Api Key Middleware
app.UseMiddleware<ApiKeyMiddleware>();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
        c.DefaultModelsExpandDepth(-1); // Disable schema models at the bottom of the Swagger UI
    });
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();