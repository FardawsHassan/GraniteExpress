using GraniteExpress.Data;
using GraniteExpress.Models;

namespace GraniteExpress.Services
{
    public interface IAccountService
    {
        Task<List<Account>> GetAccounts();
    }

    public class AccountService : IAccountService
    {
        private readonly ApplicationDbContext _context;

        public AccountService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Account>> GetAccounts()
        {
            return _context.RefAccount.ToList();
        }
    }
}
