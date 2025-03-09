using Newtonsoft.Json.Linq;
using StockTracker2.Interfaces;
using StockTracker2.Models;

namespace StockTracker2.Services
{
   
        public class StockService : IStockService
    {
            private readonly string _apiKey = "92ZDWNO5U6TT7WOU"; // Replace with your Alpha Vantage API key
            private readonly HttpClient _httpClient;

            public StockService()
            {
                _httpClient = new HttpClient();
            }

        public async Task<List<Stocks>> GetStocksAsync(List<string> symbols)
        {
            var stocks = new List<Stocks>();

            foreach (var symbol in symbols)
            {
                try
                {
                    var apiUrl = $"https://www.alphavantage.co/query?function=GLOBAL_QUOTE&symbol={symbol}&apikey={_apiKey}";

                    var response = await _httpClient.GetStringAsync(apiUrl);
                    var data = JObject.Parse(response)["Global Quote"];

                    if (data != null)
                    {
                        var stock = new Stocks
                        {
                            Symbol = data["01. symbol"]?.ToString(),
                            Price = decimal.Parse(data["05. price"]?.ToString() ?? "0"),
                            ChangePercent = decimal.Parse(data["10. change percent"]?.ToString().TrimEnd('%') ?? "0"),
                            CompanyName = symbol,
                            MarketCap = 0,
                            P_E = (decimal)await GetPERatioAsync(symbol),
                            EPS = 0,
                            EPSGrowth = 0,
                            DivYield = 0,
                            Sector = "Unknown",
                            AnalystRating = "N/A"
                        };

                        stocks.Add(stock);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error fetching data for {symbol}: {ex.Message}");
                }
            }

            return stocks;
        }
        public async Task<decimal?> GetPERatioAsync(string symbol)
        {
            try
            {
                // Step 1: Get the Price
                var priceResponse = await _httpClient.GetStringAsync(
                    $"https://www.alphavantage.co/query?function=GLOBAL_QUOTE&symbol={symbol}&apikey={_apiKey}"
                );
                var priceData = JObject.Parse(priceResponse)["Global Quote"];
                var price = decimal.Parse(priceData["05. price"]?.ToString() ?? "0");

                // Step 2: Get the EPS
                var earningsResponse = await _httpClient.GetStringAsync(
                    $"https://www.alphavantage.co/query?function=EARNINGS&symbol={symbol}&apikey={_apiKey}"
                );
                var earningsData = JObject.Parse(earningsResponse);
                var eps = decimal.Parse(earningsData["annualEarnings"]?[0]?["reportedEPS"]?.ToString() ?? "0");

                // Step 3: Calculate P/E Ratio
                if (eps > 0)
                {
                    return price / eps;
                }

                return null; // EPS is 0 or negative, so P/E is not meaningful
            }
            catch
            {
                return null; // Handle errors gracefully
            }
        }

    }

}

