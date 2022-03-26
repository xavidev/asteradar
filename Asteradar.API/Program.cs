using System.ComponentModel.DataAnnotations;
using Asteradar.API.Client;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddHttpClient<AsteroidClient>(x =>
{
    x.BaseAddress = new Uri(builder.Configuration["API:BaseUrl"]);
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapGet("/asteroids", (string planet) => Results.Ok()).ProducesValidationProblem(400);

app.Run();

public partial class Program { }