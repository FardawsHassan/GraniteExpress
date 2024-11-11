using AutoMapper;
using GraniteExpress.Data;
using GraniteExpress.DtoModels;
using Microsoft.EntityFrameworkCore;

namespace GraniteExpress.Services
{
    public interface IAccountService
    {
        Task<List<AccountDto>> GetAccounts();
    }

    public class AccountService : IAccountService
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly ILogger<AccountService> _logger;

        public AccountService(ApplicationDbContext context, IMapper mapper, ILogger<AccountService> logger)
        {
            _context = context;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<List<AccountDto>> GetAccounts()
        {
            try
            {
                var accounts = await _context.RefAccount.ToListAsync();
                return _mapper.Map<List<AccountDto>>(accounts);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Method->AccountService Error->{ex.Message}");
                return null;
            }
        }
    }
}
