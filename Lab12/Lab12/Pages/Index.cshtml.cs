using Lab12.Services;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Lab12.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ExchangeRateService _currencyService;


        public IndexModel(ExchangeRateService currencyService)
        {
            _currencyService = currencyService;
        }

        public Dictionary<string, decimal> ExchangeRates { get; private set; }

        public async Task OnGetAsync()
        {
            ExchangeRates = await _currencyService.GetAllExchangeRates("USD", "UAH", "EUR");
        }
    }
}