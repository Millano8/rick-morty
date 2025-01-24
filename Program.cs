using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using System.Net.Http;

using rick_morty_webapp.Services;
using rick_morty_webapp.Services.Interfaces;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddHttpClient<IRickMortyService, RickMortyService>(client =>
{
    client.BaseAddress = new Uri("https://rickandmortyapi.com/api/");
    client.DefaultRequestHeaders.Add("Accept", "application/json");
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Rick and Morty API",
        Version = "v1",
        Description = "API para obtener información de personajes de Rick and Morty"
    });
});



//builder.WebHost.UseKestrel(options =>
//{
//    options.ListenAnyIP(5000);
//});

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", builder =>
    {
        builder.AllowAnyOrigin()
               .AllowAnyMethod()
               .AllowAnyHeader();
    });
});

var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    // Habilitar Swagger en entorno de desarrollo
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Rick and Morty API v1"));
}

// Habilitar CORS
app.UseCors("AllowAll");


app.UseHttpsRedirection();

app.UseRouting();

app.UseAuthorization();

app.MapControllers();

app.Run();
