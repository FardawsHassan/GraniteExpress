using GraniteExpress.Data;
using GraniteExpress.Models;

namespace GraniteExpress.Services
{
    public interface ICurrencyService
    {
        Task<List<Currency>> GetCurrencies();
    }

    public class CurrencyService : ICurrencyService
    {
        private readonly ApplicationDbContext _context;

        public CurrencyService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Currency>> GetCurrencies()
        {
            return _context.RefCurrency.ToList();
        }
    }
}
