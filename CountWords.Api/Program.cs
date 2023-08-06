using CountWords.Services.Services.Implementation;
using CountWords.Services.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddHttpClient("FindTextServiceClient", client => {
    client.BaseAddress = new Uri(Environment.GetEnvironmentVariable("url"));

});

builder.Services.AddTransient<IWikiClientService, WikiClientService>();
builder.Services.AddTransient<ICountWordsService, CountWordsService>();
builder.Services.AddTransient<IWordsService, WordsService>();






var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    Console.WriteLine("url =  " + Environment.GetEnvironmentVariable("url"));
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
