using GraniteExpress.Data;
using GraniteExpress.Models;

namespace GraniteExpress.Services
{
    public interface IClientService
    {
        Task<List<Client>> GetClients();
    }

    public class ClientService : IClientService
    {
        private readonly ApplicationDbContext _context;

        public ClientService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Client>> GetClients()
        {
            var clients =  _context.RefClient.ToList();
            return clients;
        }
    }
}
