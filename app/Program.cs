using DolarAPI;
using System;
using System.Text.Json;

var builder = WebApplication.CreateBuilder(args);
var  MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins,
                      policy  =>
                      {
                          policy.WithOrigins("http://localhost:5173", "http://192.168.0.109:5173").AllowAnyHeader().AllowAnyMethod();
                      });
});
// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();
app.UseCors(MyAllowSpecificOrigins);
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
HttpClient client = new();
WebScraper ws = new(client);
/*Everytime the server starts it has to scratch the website and get the currency values.
In order to be more efficient it should store somewhere the values.
*/
string dolar_string = await ws.GetCurrency("https://www.bcv.org.ve/", "dolar");
dolar_string = dolar_string.Replace(',', '.');
float dolar = float.Parse(dolar_string);
string euro_string = await ws.GetCurrency("https://www.bcv.org.ve/", "euro");
euro_string = euro_string.Replace(',', '.');
float euro = float.Parse(euro_string);
string yuan_string = await ws.GetCurrency("https://www.bcv.org.ve/", "yuan");
yuan_string = yuan_string.Replace(',', '.');
float yuan = float.Parse(yuan_string);
List<CurrencyResponse> currencies = [new CurrencyResponse{Name = "Dólar",Price = dolar}, new CurrencyResponse{Name = "Euro",Price = euro}, new CurrencyResponse{Name = "Yuan",Price = yuan}];

app.MapGet("/v2/currencies", async () =>
{
    return currencies;
});

app.MapGet("/v2/dolar", async () =>
{
    return currencies.Find(element => element.Name == "Dólar");
});
app.MapGet("/v2/euro", async () =>
{
    return currencies.Find(element => element.Name == "Euro");
});
app.MapGet("/v2/yuan", async () =>
{
    return currencies.Find(element => element.Name == "Yuan");
});

app.UseHttpsRedirection();

app.Run();

