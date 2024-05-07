using GraniteExpress.Data;
using GraniteExpress.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace GraniteExpress.Services
{
    public interface IJournalService
    {
        Task<List<Journal>> GetJournals();
        Task<Journal> GetJournalById(int journalId);
        Task<List<JournalView>> GetJournalViews();
        Task<Journal> SaveJournal(Journal journal);
        Task<bool> DeleteJournal(int journalId);
    }

    public class JournalService : IJournalService
    {
        private readonly ApplicationDbContext _context;

        public JournalService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Journal>> GetJournals()
        {
            return _context.GenJournal.Include(x => x.JournalDetail).ToList();
        }
        
        public async Task<Journal> GetJournalById(int journalId)
        {
            var journal =  _context.GenJournal.Where(j => j.JournalId == journalId).Include(x => x.JournalDetail).FirstOrDefault();
            journal.JournalDetail.RemoveAll(x => x.JournalId == 0);
            return journal;
        }
        
        public async Task<bool> DeleteJournal(int journalId)
        {
            try
            {
                var journal =  _context.GenJournal.Where(j => j.JournalId == journalId).FirstOrDefault();
                journal.IsDelete = true;
                _context.Update(journal);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }

        }
        
        public async Task<List<JournalView>> GetJournalViews()
        {


            // Assuming _context is your _context instance
            var query = from journal in _context.GenJournal where journal.IsDelete == false
                        join journalDetails in _context.GenJournalDetails on journal.JournalId equals journalDetails.JournalId
                        join account in _context.RefAccount on journalDetails.AccountId equals account.AccountId
                        join client in _context.RefClient on journal.ClientId equals client.ClientId
                        join documentType in _context.RefDocumentType on journal.DocumentTypeId equals documentType.DocumentTypeId
                        select new
                        {
                            journal.DocumentTypeId,
                            journal.DocumentNo,
                            journal.JournalId,
                            journal.JournalDescription,
                            journal.DocumentDate,
                            account.AccountCode,
                            account.IsActive,
                            client.ClientFirstName,
                            client.ClientLastName,
                            journalDetails.IsDebit,
                            journalDetails.CurrencyAmount,
                            journalDetails.ExchangeRate,
                            documentType.DocumentName
                        };

            var result = query.ToList();

            List<JournalView> journalViews = new();
            foreach (var item in result)
            {
                journalViews.Add(new JournalView { 
                    DocumentTypeId = item.DocumentTypeId,
                    DocumentNo = item.DocumentNo,
                    JournalDescription = item.JournalDescription,
                    DocumentDate = item.DocumentDate,
                    AccountCode = item.AccountCode,
                    IsActive = item.IsActive,
                    ClientFirstName = item.ClientFirstName,
                    ClientLastName = item.ClientLastName,
                    IsDebit = item.IsDebit,
                    CurrencyAmount = item.CurrencyAmount,
                    ExchangeRate = item.ExchangeRate,
                    DocumentName = item.DocumentName,
                    JournalId = item.JournalId
                });
            }

            return journalViews;
        }

        public async Task<Journal> SaveJournal(Journal journal)
        {
            try
            {
                var result = _context.GenJournal.Where(x => x.JournalId == journal.JournalId).Include(j => j.JournalDetail).FirstOrDefault();
                if(result is null)
                {
                    var response =  await _context.GenJournal.AddAsync(journal);
                    await _context.SaveChangesAsync();

                    journal.JournalId = response.Entity.JournalId;
                    return journal;
                }
                else
                {
                    var response = _context.GenJournal.Update(journal);
                    await _context.SaveChangesAsync();

                    return journal;
                }
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
