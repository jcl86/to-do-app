using Microsoft.Net.Http.Headers;
using ToDo.Host;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSingleton<ITodosRepository, InMemoryTodosRepository>();
builder.Services.AddCors();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapEndpoints();

app.UseCors(policy =>
        policy.WithOrigins("https://localhost:5011", "http://localhost:9000")
        .AllowAnyMethod()
        .WithHeaders(HeaderNames.ContentType, HeaderNames.Authorization)
        .AllowCredentials());


app.Run();

