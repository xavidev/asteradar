using Asteradar.API.Client;
using Asteradar.API.Service;

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

app.MapGet("/asteroids", (string planet) =>
{
    
    return Results.Ok();
}).ProducesValidationProblem(400);

app.Run();

public partial class Program { }