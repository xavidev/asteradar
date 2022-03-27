using Asteradar.API.Client;
using Asteradar.API.Service;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddHttpClient<IAsteroidClient, AsteroidClient>(x =>
{
    x.BaseAddress = new Uri(builder.Configuration["API:BaseUrl"]);
});
builder.Services.AddScoped<AsteroidFinder>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapGet("/asteroids", async (string planet, AsteroidFinder finder) =>
{
    try
    {
        var result = await finder.GetHazardousAsteroids(planet);
        return Results.Ok(result);
    }
    catch (ArgumentException)
    {
        return Results.BadRequest(new ValidationProblemDetails(new Dictionary<string, string[]>()
        {
            ["Planet"] = new []{"Planet must be a valid name."}
        }));
    }
    
})
    .WithName("GetHazardousAsteroids")
    .Produces<List<AsteroidDTO>>()
    .ProducesValidationProblem(400);

app.Run();

public partial class Program { }