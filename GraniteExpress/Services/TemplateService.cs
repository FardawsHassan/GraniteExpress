using GraniteExpress.Data;
using GraniteExpress.Models;
using Microsoft.EntityFrameworkCore;

namespace GraniteExpress.Services
{
    public interface ITemplateService
    {
        Task<List<Template>> GetTemplates();
    }

    public class TemplateService : ITemplateService
    {
        private readonly ApplicationDbContext _context;

        public TemplateService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Template>> GetTemplates()
        {
            return  _context.RefTemplate.Include(x => x.TemplateDetail).ThenInclude(x => x.Account).ThenInclude(x => x.Currency).ToList();
        }
    }
}
