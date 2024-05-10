using GraniteExpress.Data;
using GraniteExpress.Models;

namespace GraniteExpress.Services
{
    public interface ICashFlowService
    {
        Task<List<CashFlow>> GetCashFlows();
    }

    public class CashFlowService : ICashFlowService
    {
        private readonly ApplicationDbContext _context;

        public CashFlowService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<CashFlow>> GetCashFlows()
        {
            var CashFlows =  _context.RefCashFlow.ToList();
            return CashFlows;
        }
    }
}
