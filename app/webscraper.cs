using HtmlAgilityPack;

namespace DolarAPI
{
    public class WebScraper(HttpClient client)
    {
        string response = "";
        string currency = "";
        public async Task<string> GetCurrency(string url, string currency)
        {
            
            try
            {
                HttpResponseMessage r = await client.GetAsync(url);
                r.EnsureSuccessStatusCode();
                response = await r.Content.ReadAsStringAsync();
                HtmlDocument doc = new();
                doc.LoadHtml(response);
                var body = doc.DocumentNode.SelectSingleNode($"//div[@id='{currency}']//strong");
                currency = body.InnerText;
                
            }
            catch (HttpRequestException e)
            {
                Console.WriteLine("\nException Caught!");
                Console.WriteLine("Message :{0} ", e.Message);
            }
            return currency;
        }
    }
}