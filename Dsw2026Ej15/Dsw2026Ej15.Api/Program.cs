using Dsw2026Ej15.Api.Middleware;
using Dsw2026Ej15.Data;
using Dsw2026Ej15.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using static System.Net.Mime.MediaTypeNames;

namespace Dsw2026Ej15.Api;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        var connectionsString = " Data Source = (localdb)\\MSSQLLocalDB; Integrated Security = True; Connect Timeout = 30; Encrypt = True; Trust Server Certificate = true" ;
        builder.Services.AddDbContext<Dsw2026Ej15DbContext>(options=>
        {
            options.UseSqlServer();
        });
        builder.Services.AddControllers();
        builder.Services.AddSwaggerGen();
        builder.Services.AddSingleton<IPersistence, PersistenceInMemory>();
        builder.Services.AddHealthChecks();
        var app = builder.Build();

        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }
       
        app.UseMiddleware<ExceptionHandlingMiddleware>();
        app.UseAuthorization();
        app.MapGet("/health-check", () => Results.Ok("Healthy"));
        app.MapControllers();
        app.Run();
    }
}