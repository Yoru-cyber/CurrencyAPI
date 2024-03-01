using DolarAPI;

var builder = WebApplication.CreateBuilder(args);
var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins,
                      policy =>
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
Func<Task<float>> dolar_f = async () =>
{
    var r = await ws.GetCurrency("https://www.bcv.org.ve/", "dolar");
    r = r.Replace(',', '.');
    return float.Parse(r);
};
Func<Task<float>> euro_f = async () =>
{
    var r = await ws.GetCurrency("https://www.bcv.org.ve/", "euro");
    r = r.Replace(',', '.');
    return float.Parse(r);
};
Func<Task<float>> yuan_f = async () =>
{
    var r = await ws.GetCurrency("https://www.bcv.org.ve/", "yuan");
    r = r.Replace(',', '.');
    return float.Parse(r);
};
float dolar = dolar_f().Result;
float euro = euro_f().Result;
float yuan = yuan_f().Result;

List<CurrencyResponse> currencies = [new CurrencyResponse("Dólar", dolar), new CurrencyResponse("Euro", euro), new CurrencyResponse("Yuan", yuan)];
//Redirect to documentation
app.MapGet("/", () =>
{
    return Results.Redirect("/swagger/index.html", permanent: true);
});
app.MapGet("/v2/currencies", () =>
{
    return currencies;
});

app.MapGet("/v2/dolar", () =>
{
    return currencies.Find(element => element.Name == "Dólar");
});
app.MapGet("/v2/euro", () =>
{
    return currencies.Find(element => element.Name == "Euro");
});
app.MapGet("/v2/yuan", () =>
{
    return currencies.Find(element => element.Name == "Yuan");
});

app.UseHttpsRedirection();

app.Run();

