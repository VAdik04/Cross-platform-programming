using Api.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        builder.Services.AddDbContextPool<ApiContext>(optionsBuilder =>
        {
            AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
            var dbType =
                builder.Configuration["DbType"]
                ?? throw new InvalidOperationException(
                    "Configuration does not contain database type."
                );
            var connectionString =
                builder.Configuration.GetConnectionString("Default")
                ?? throw new InvalidOperationException(
                    "Configuration does not contain connection string 'Default'."
                );
            ConfigureDbContext(optionsBuilder, dbType, connectionString);
        });

        builder.Services.AddAuthentication()
            .AddJwtBearer(options =>
            {
                options.Authority = "http://identity_server:5001";
                options.TokenValidationParameters.ValidateIssuer = false;
                options.TokenValidationParameters.ValidateAudience = false;
                options.RequireHttpsMetadata = false;
            });
        builder.Services.AddControllers();
        builder.Services.AddApiVersioning(options =>
        {
            options.AssumeDefaultVersionWhenUnspecified = true;
            options.DefaultApiVersion = new ApiVersion(1, 0);
        });

        var app = builder.Build();
        using (var scope = app.Services.CreateScope())
        {
            using var context = scope.ServiceProvider.GetRequiredService<ApiContext>();
            if (context.Database.EnsureCreated())
            {
                DataSeeder.Seed(context, 100);
            }
        }
        app.UseAuthentication();
        app.UseAuthorization();
        app.MapControllers()
            .RequireAuthorization();
        app.Run();
    }


    private static void ConfigureDbContext(
        DbContextOptionsBuilder optionsBuilder,
        string dbType,
        string connectionString
    )
    {
        switch (dbType.ToLower())
        {
            case "in-memory":
                optionsBuilder.UseInMemoryDatabase(connectionString);
                break;
            case "sqlite":
                optionsBuilder.UseSqlite(connectionString);
                break;
            case "ms-sql":
                optionsBuilder.UseSqlServer(connectionString);
                break;
            case "postgres":
                optionsBuilder.UseNpgsql(connectionString);
                break;
            default:
                throw new InvalidOperationException("Invalid database type specified in configuration.");
        }
    }
}