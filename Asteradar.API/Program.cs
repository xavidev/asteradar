using Asteradar.API.Client;

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

app.MapGet("/", () => "Hello World!");

app.Run();

public partial class Program { }