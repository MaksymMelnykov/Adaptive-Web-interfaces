using Microsoft.AspNetCore.SignalR;

namespace Lab12.Hubs
{
    public class ExchangeRateHub : Hub
    {
        public async Task SendExchangeRates(Dictionary<string, Dictionary<string, decimal>> exchangeRates)
        {
            await Clients.All.SendAsync("ReceiveExchangeRates", exchangeRates);
        }
    }
}
