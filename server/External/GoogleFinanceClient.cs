using System.Net.Http;
using System.Threading.Tasks;
using HtmlAgilityPack;
using System.Net;
using System.Globalization;

namespace MyPostgresApi.External;

public class GoogleFinanceClient
{
  private readonly HttpClient _http;

  public GoogleFinanceClient(HttpClient http)
  {
    _http = http;
    _http.DefaultRequestHeaders.UserAgent.ParseAdd("Mozilla/5.0 (Windows NT 10.0; Win64; x64)");
  }

  // Return price of an asset - currently only supports Canadian-listed assets (TSE & CAD)
  public async Task<float?> GetPriceAsync(string symbol, string type)
  {
    var suffix = type == "stock" ? ":TSE" : "-CAD";
    var url = $"https://www.google.com/finance/quote/{symbol}{suffix}";
    Console.WriteLine("", url, suffix);

    var res = await _http.GetAsync(url);
    if (!res.IsSuccessStatusCode)
    {
      Console.WriteLine($"Failed to fetch: {url} - Status {res.StatusCode}");
      return null;
    }

    var html = await res.Content.ReadAsStringAsync();

    var doc = new HtmlDocument();
    doc.LoadHtml(html);

    // Google's price is in a <div> with class like: "YMlKec fxKbKc"
    var priceNode = doc.DocumentNode.SelectSingleNode("//div[contains(@class,'YMlKec') and contains(@class,'fxKbKc')]");

    if (priceNode != null)
    {
      var raw = WebUtility.HtmlDecode(priceNode.InnerText).Trim().Replace("$", "");
      if (float.TryParse(raw, NumberStyles.Any, CultureInfo.InvariantCulture, out var price))
      {
        return price;
      }
    }

    Console.WriteLine("Price not found or parse failed.");
    return null;
  }
}
