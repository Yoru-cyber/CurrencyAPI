using DolarAPI;
using System;
using System.Text.Json;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
HttpClient client = new();
WebScraper ws = new(client);
string dolar_string = await ws.GetCurrency("https://www.bcv.org.ve/", "dolar");
dolar_string = dolar_string.Replace(',', '.');
float dolar = float.Parse(dolar_string);
string euro_string = await ws.GetCurrency("https://www.bcv.org.ve/", "euro");
euro_string = euro_string.Replace(',', '.');
float euro = float.Parse(euro_string);
app.MapGet("/v2/dolar", async () =>
{
    var data = new CurrencyResponse
    {
        Price = dolar
    };
    string response = JsonSerializer.Serialize(data);
    return response;
});
app.MapGet("/v2/euro", async () =>
{

    return euro;
});

app.UseHttpsRedirection();


app.Run();

