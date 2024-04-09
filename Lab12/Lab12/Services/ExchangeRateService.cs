using Newtonsoft.Json.Linq;


namespace Lab12.Services
{
    public class ExchangeRateService
    {
        private readonly HttpClient _httpClient;

        public ExchangeRateService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<Dictionary<string, decimal>> GetAllExchangeRates(params string[] baseCurrencies)
        {
            try
            {
                var result = new Dictionary<string, decimal>();
                foreach (var baseCurrency in baseCurrencies)
                {
                    var response = await _httpClient.GetAsync($"https://v6.exchangerate-api.com/v6/1aa452d30d3d382c9bd73cea/latest/{baseCurrency}");
                    response.EnsureSuccessStatusCode();
                    var jsonString = await response.Content.ReadAsStringAsync();
                    var data = JObject.Parse(jsonString);
                    var rates = data["conversion_rates"].ToObject<Dictionary<string, decimal>>();
                    foreach (var rate in rates)
                    {
                        result[$"{baseCurrency}/{rate.Key}"] = rate.Value;
                    }
                }
                return result;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error fetching exchange rates: " + ex.Message);
                throw;
            }
        }
    }
}
