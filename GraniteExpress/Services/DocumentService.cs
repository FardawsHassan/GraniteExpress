using GraniteExpress.Data;
using GraniteExpress.Models;

namespace GraniteExpress.Services
{
    public interface IDocumentService
    {
        Task<List<DocumentType>> GetDocumentTypes();
    }

    public class DocumentService : IDocumentService
    {
        private readonly ApplicationDbContext _context;

        public DocumentService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<DocumentType>> GetDocumentTypes()
        {
            var documentTypes =  _context.RefDocumentType.ToList();
            return documentTypes;
        }
    }
}
