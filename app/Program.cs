using DolarAPI;
using System;

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
app.MapGet("/v2/dolar", async () => 
{
    HttpClient client = new();
    WebScraper ws = new(client);
    string dolar_string = await ws.GetCurrency("https://www.bcv.org.ve/", "dolar");
    dolar_string = dolar_string.Replace(',', '.');
    float dolar = float.Parse(dolar_string);
    return dolar;
});
app.UseHttpsRedirection();


app.Run();

