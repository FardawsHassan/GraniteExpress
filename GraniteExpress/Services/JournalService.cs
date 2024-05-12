using GraniteExpress.Components.Pages;
using GraniteExpress.Data;
using GraniteExpress.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using static MudBlazor.CategoryTypes;

namespace GraniteExpress.Services
{
    public interface IJournalService
    {
        Task<List<Journal>> GetJournals();
        Task<Journal> GetJournalById(int journalId);
        Task<List<Models.JournalView>> GetJournalViews();
        Task<bool> SaveJournal(Journal journal);
        Task<bool> DeleteJournal(int journalId);
        List<SelectJournalView> JournalSelect(DateTime? start, DateTime? end);
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
                //var journal =  _context.GenJournal.Where(j => j.JournalId == journalId).AsNoTracking().FirstOrDefault();
                //_context.Entry(journal).State = EntityState.Detached;
                //journal.IsDelete = true;
                //_context.GenJournal.Update(journal);
                //await _context.SaveChangesAsync();
                //return true;

                //string sql = $"UPDATE GenJournal SET 'IsDelete' = 1 WHERE 'JournalId' = {journalId}";

                List<SqlParameter> parms = new List<SqlParameter>
                {

                };

                var result = await _context.Database.ExecuteSqlAsync($"UPDATE GenJournal SET IsDelete = 1 WHERE JournalId = {journalId}");
                return result > 0 ? true : false;
                //return _context.Database.SqlQueryRaw<SelectJournalView>(sql, parms.ToArray()).ToList();
            }
            catch (Exception)
            {
                return false;
            }

        }
        
        public async Task<List<Models.JournalView>> GetJournalViews()
        {
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
            List<Models.JournalView> journalViews = new();
            foreach (var item in result)
            {
                journalViews.Add(new Models.JournalView { 
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

        public async Task<bool> SaveJournal(Journal journal)
        {
            try
            {
                var result = _context.GenJournal
                    .Where(x => x.JournalId == journal.JournalId)
                    .FirstOrDefault();

                if (result is null)
                {
                    var newJournal = new Journal
                    {
                        JournalDescription = journal.JournalDescription,
                        DocumentTypeId = journal.DocumentTypeId,
                        ClientId = journal.ClientId,
                        DocumentNo = journal.DocumentNo,
                        DocumentDate = journal.DocumentDate,
                        UserId = journal.UserId,
                        TemplateId = journal.TemplateId
                    };
                    await _context.GenJournal.AddAsync(newJournal);
                    await _context.SaveChangesAsync();
                    journal.JournalId = newJournal.JournalId;
                    var journalDetails = journal.JournalDetail;
                    journalDetails.ForEach(x => x.JournalId = journal.JournalId);
                    await _context.GenJournalDetails.AddRangeAsync(journalDetails);
                    await _context.SaveChangesAsync();
                    return true;
                }
                else
                {
                    result.JournalDescription = journal.JournalDescription;
                    result.DocumentTypeId = journal.DocumentTypeId;
                    result.ClientId = journal.ClientId;
                    result.DocumentNo = journal.DocumentNo;
                    result.DocumentDate = journal.DocumentDate;
                    result.UserId = journal.UserId;
                    result.TemplateId = journal.TemplateId;

                    var journalDetails = await _context.GenJournalDetails.Where(x=>x.JournalId== journal.JournalId).ToListAsync();

                    List<JournalDetail> newJournalDetails = new();
                    foreach (var detail in journal.JournalDetail)
                    {
                        var hasJournal = journalDetails.Where(x => x.JournalDetailId == detail.JournalDetailId).FirstOrDefault();
                        if(hasJournal != null)
                        {
                            hasJournal.AccountId = detail.AccountId;
                            hasJournal.CashFlowId = detail.CashFlowId;
                            hasJournal.ClientId = detail.ClientId;
                            hasJournal.ExchangeRate = detail.ExchangeRate;
                            hasJournal.CurrencyAmount = detail.CurrencyAmount;
                            hasJournal.IsDebit = detail.IsDebit;
                            newJournalDetails.Add(hasJournal);
                        }
                        else
                        {
                            detail.JournalId = journal.JournalId;
                            newJournalDetails.Add(detail);
                        }
                    }

                    result.JournalDetail = newJournalDetails;

                    _context.GenJournal.Update(result);

                    await _context.SaveChangesAsync();

                    return true;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }

        public List<SelectJournalView> JournalSelect(DateTime? start, DateTime? end)
        {
            try
            {
                start ??= DateTime.Now;
                end ??= DateTime.Now;

                List<SelectJournalView> list;
                string sql = "EXEC select_journal @bdate, @edate";

                List<SqlParameter> parms = new List<SqlParameter>
                {
                    // Create parameter(s)    
                    new SqlParameter { ParameterName = "@edate", Value = end },
                    new SqlParameter { ParameterName = "@bdate", Value = start }
                };

                return _context.Database.SqlQueryRaw<SelectJournalView>(sql, parms.ToArray()).ToList();
            }
            catch (Exception)
            {

                return new();
            }
        }
    }
}
