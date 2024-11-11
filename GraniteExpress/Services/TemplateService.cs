using AutoMapper;
using GraniteExpress.Data;
using GraniteExpress.DtoModels;
using Microsoft.EntityFrameworkCore;

namespace GraniteExpress.Services
{
    public interface ITemplateService
    {
        Task<List<TemplateDto>> GetTemplates();
    }

    public class TemplateService : ITemplateService
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly ILogger<TemplateService> _logger;

        public TemplateService(ApplicationDbContext context, IMapper mapper, ILogger<TemplateService> logger)
        {
            _context = context;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<List<TemplateDto>> GetTemplates()
        {
            try
            {
                var templates = await _context.RefTemplate.Include(x => x.TemplateDetail).ThenInclude(x => x.Account).ThenInclude(x => x.Currency).ToListAsync();
                return _mapper.Map<List<TemplateDto>>(templates);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Method->GetTemplates Error->{ex.Message}");
                return null;
            }
        }
    }
}
