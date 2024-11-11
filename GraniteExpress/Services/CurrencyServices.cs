using AutoMapper;
using GraniteExpress.Data;
using GraniteExpress.DtoModels;
using Microsoft.EntityFrameworkCore;

namespace GraniteExpress.Services
{
    public interface ICurrencyService
    {
        Task<List<CurrencyDto>> GetCurrencies();
    }

    public class CurrencyService : ICurrencyService
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly ILogger<CurrencyService> _logger;

        public CurrencyService(ApplicationDbContext context, IMapper mapper, ILogger<CurrencyService> logger)
        {
            _context = context;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<List<CurrencyDto>> GetCurrencies()
        {
            try
            {
                var refCurrencies = await _context.RefCurrency.ToListAsync();
                return _mapper.Map<List<CurrencyDto>>(refCurrencies);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Method->GetCurrencies Error->{ex.Message}");
                return null;
            }
        }
    }
}
