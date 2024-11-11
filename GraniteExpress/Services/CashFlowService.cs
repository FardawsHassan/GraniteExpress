using AutoMapper;
using GraniteExpress.Data;
using GraniteExpress.DtoModels;
using Microsoft.EntityFrameworkCore;

namespace GraniteExpress.Services
{
    public interface ICashFlowService
    {
        Task<List<CashFlowDto>> GetCashFlows();
    }

    public class CashFlowService : ICashFlowService
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly ILogger<CashFlowService> _logger;

        public CashFlowService(ApplicationDbContext context, IMapper mapper, ILogger<CashFlowService> logger)
        {
            _context = context;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<List<CashFlowDto>> GetCashFlows()
        {
            try
            {
                var cashFlows =  await _context.RefCashFlow.ToListAsync();
                return _mapper.Map<List<CashFlowDto>>(cashFlows);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Method->GetCashFlows Error->{ex.Message}");
                return null;
            }
        }
    }
}
