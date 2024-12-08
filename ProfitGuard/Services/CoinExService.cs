using System.Net.Http;
using System.Text.Json;
using System.Text;
using System.Threading.Tasks;

public class CoinExService
{
    private readonly HttpClient _httpClient;
    private readonly string _apiKey = "YOUR_API_KEY";
    private readonly string _apiSecret = "YOUR_API_SECRET";

    public CoinExService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<bool> BuyBitcoin(decimal amountInUSD)
    {
        var endpoint = "https://api.coinex.com/v1/order/limit";
        var price = await GetCurrentPrice();
        var data = new
        {
            market = "BTCUSDT",
            type = "buy",
            amount = amountInUSD / price,
            price
        };

        var payload = JsonSerializer.Serialize(data);
        var content = new StringContent(payload, Encoding.UTF8, "application/json");

        _httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {_apiKey}");

        var response = await _httpClient.PostAsync(endpoint, content);

        return response.IsSuccessStatusCode;
    }

    public async Task<decimal> GetCurrentPrice()
    {
        var endpoint = "https://api.coinex.com/v1/market/ticker?market=BTCUSDT";
        var response = await _httpClient.GetAsync(endpoint);
        var json = await response.Content.ReadAsStringAsync();
        var data = JsonSerializer.Deserialize<CoinExTickerResponse>(json);
        return data?.Ticker.Last ?? 0;
    }

    private class CoinExTickerResponse
    {
        public TickerData Ticker { get; set; }
    }

    private class TickerData
    {
        public decimal Last { get; set; }
    }
}
