﻿using Microsoft.EntityFrameworkCore;
using OFoodBackendTask.Models;

namespace OFoodBackendTask;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // database
        var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
        builder.Services.AddDbContext<OfoodContext>(options =>
            options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));

        // Add services to the container.
        builder.Services.AddAuthorization();
        builder.Services.AddControllersWithViews();



        var app = builder.Build();

        // Configure the HTTP request pipeline.

        app.UseAuthorization();
        app.UseRouting();
        app.MapControllers();

        var summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        //app.MapGet("/weatherforecast", (HttpContext httpContext) =>
        //{
        //    var forecast =  Enumerable.Range(1, 5).Select(index =>
        //        new WeatherForecast
        //        {
        //            Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
        //            TemperatureC = Random.Shared.Next(-20, 55),
        //            Summary = summaries[Random.Shared.Next(summaries.Length)]
        //        })
        //        .ToArray();
        //    return forecast;
        //});

        app.Run();
    }
}

