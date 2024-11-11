using AutoMapper;
using GraniteExpress.Data;
using GraniteExpress.DtoModels;
using Microsoft.EntityFrameworkCore;

namespace GraniteExpress.Services
{
    public interface IDocumentService
    {
        Task<List<DocumentTypeDto>> GetDocumentTypes();
    }

    public class DocumentService : IDocumentService
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly ILogger<DocumentService> _logger;

        public DocumentService(ApplicationDbContext context, IMapper mapper, ILogger<DocumentService> logger)
        {
            _context = context;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<List<DocumentTypeDto>> GetDocumentTypes()
        {
            try
            {
                var documentTypes =  await _context.RefDocumentType.ToListAsync();
                return _mapper.Map<List<DocumentTypeDto>>(documentTypes);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Method->GetDocumentTypes Error->{ex.Message}");
                return null;
            }
        }
    }
}
