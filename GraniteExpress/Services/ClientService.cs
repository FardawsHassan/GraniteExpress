using AutoMapper;
using GraniteExpress.Data;
using GraniteExpress.DtoModels;
using Microsoft.EntityFrameworkCore;

namespace GraniteExpress.Services
{
    public interface IClientService
    {
        Task<List<ClientDto>> GetClients();
    }

    public class ClientService : IClientService
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly ILogger<ClientService> _logger;

        public ClientService(ApplicationDbContext context, IMapper mapper, ILogger<ClientService> logger)
        {
            _context = context;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<List<ClientDto>> GetClients()
        {
            try
            {
                var clients =  await _context.RefClient.ToListAsync();
                return _mapper.Map<List<ClientDto>>(clients);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Method->GetClients Error->{ex.Message}");
                return null;
            }
        }
    }
}
