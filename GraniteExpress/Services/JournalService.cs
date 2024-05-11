using GraniteExpress.Components.Pages;
using GraniteExpress.Data;
using GraniteExpress.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Linq;

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
                var result = _context.GenJournal.Where(x => x.JournalId == journal.JournalId).FirstOrDefault();
                if (result is null)
                {
                    //var newJournal = new Journal
                    //{
                    //    JournalDescription = journal.JournalDescription,
                    //    DocumentTypeId = journal.DocumentTypeId,
                    //    ClientId = journal.ClientId,
                    //    DocumentNo = journal.DocumentNo,
                    //    DocumentDate = journal.DocumentDate,
                    //    UserId = journal.UserId,
                    //    TemplateId = journal.TemplateId
                    //};
                    //var response = await _context.GenJournal.AddAsync(newJournal);
                    //await _context.SaveChangesAsync();

                    List<Journal> list;
                    string sql = "EXEC AddJournal @TemplateId, @DocumentDate,@JournalDescription,@DocumentTypeId,@ClientId,@DocumentNo,@UserId,0";

                    List<SqlParameter> journalParams = new List<SqlParameter>
                    {
                        // Create parameter(s)    
                        new SqlParameter { ParameterName = "@JournalDescription", Value = journal.JournalDescription },
                        new SqlParameter { ParameterName = "@DocumentTypeId", Value = journal.DocumentTypeId },
                        new SqlParameter { ParameterName = "@ClientId", Value = journal.ClientId },
                        new SqlParameter { ParameterName = "@DocumentTypeId", Value = journal.DocumentTypeId },
                        new SqlParameter { ParameterName = "@DocumentNo", Value = journal.DocumentNo },
                        new SqlParameter { ParameterName = "@DocumentDate", Value = journal.DocumentDate },
                        new SqlParameter { ParameterName = "@UserId", Value = journal.UserId },
                        new SqlParameter { ParameterName = "@TemplateId", Value = journal.TemplateId },
                    };

                    var responese = _context.Database.SqlQueryRaw<Journal>(sql, journalParams.ToArray()).ToList();


                    //journal.JournalId = response.Entity.JournalId;
                    var journalDetails = journal.JournalDetail;
                    foreach(var item in journalDetails)
                    {
                        item.JournalId = responese.FirstOrDefault().JournalId;
                        var queryResult = await _context.Database.ExecuteSqlAsync($"INSERT INTO GenJournalDetails (JournalId,AccountId,IsDebit,ClientId,CurrencyAmount,ExchangeRate,CashFlowId) VALUES ({item.JournalId} ,{item.AccountId},{item.IsDebit},{item.ClientId},{item.CurrencyAmount},{item.ExchangeRate},{item.CashFlowId})");
                    }

                    //journalDetails.ForEach(x => x.JournalId = journal.JournalId);
                    //await _context.GenJournalDetails.AddRangeAsync(journalDetails);
                    //await _context.SaveChangesAsync();
                    return true;
                }
                else
                {

                    //result.JournalDescription = journal.JournalDescription;
                    //result.DocumentTypeId = journal.DocumentTypeId;
                    //result.ClientId = journal.ClientId;
                    //result.DocumentNo = journal.DocumentNo;
                    //result.DocumentDate = journal.DocumentDate;
                    //result.UserId = journal.UserId;
                    //result.TemplateId = journal.TemplateId;
                    //_context.Entry(journal).State = EntityState.Detached;
                    //var response = _context.GenJournal.Update(result);

                    var queryResult = await _context.Database.ExecuteSqlAsync($"UPDATE GenJournal SET IsDelete = {journal.IsDelete}, JournalDescription = {journal.JournalDescription}, DocumentTypeId = {journal.DocumentTypeId}, ClientId = {journal.ClientId}, DocumentNo = {journal.DocumentNo}, TemplateId = {journal.TemplateId} WHERE JournalId = {journal.JournalId}");
                    if(queryResult == 0)
                    {
                        return false;
                    }

                    var journalDetails = _context.GenJournalDetails.Where(x => x.JournalId == journal.JournalId);
                    foreach(var item in journalDetails)
                    {
                        var updateJournal = journal.JournalDetail.Where(x => x.JournalDetailId == item.JournalDetailId).FirstOrDefault();
                        if (updateJournal is not null)
                        {
                            //item.AccountId = updateJournal.AccountId;
                            //item.CashFlowId = updateJournal.CashFlowId;
                            //item.ClientId = updateJournal.ClientId;
                            //item.ExchangeRate = updateJournal.ExchangeRate;
                            //item.CurrencyAmount = updateJournal.CurrencyAmount;
                            //item.IsDebit = updateJournal.IsDebit;
                            //_context.GenJournalDetails.Update(item);
                            queryResult = await _context.Database.ExecuteSqlAsync($"UPDATE GenJournalDetails SET AccountId = {updateJournal.AccountId}, CashFlowId = {updateJournal.CashFlowId}, ClientId = {updateJournal.ClientId}, IsDebit = {updateJournal.IsDebit}, ExchangeRate = {updateJournal.ExchangeRate}, CurrencyAmount = {updateJournal.CurrencyAmount} WHERE JournalDetailId = {updateJournal.JournalDetailId}");
                        }
                        else
                        {
                            queryResult = await _context.Database.ExecuteSqlAsync($"DELETE FROM GenJournalDetails WHERE JournalDetailId = {item.JournalDetailId}");

                            //_context.GenJournalDetails.Remove(item);
                        }
                    }

                    List<JournalDetail> newJournals = new();
                    foreach(var item in journal.JournalDetail)
                    {
                        if(!journalDetails.Any(x => x.JournalDetailId == item.JournalDetailId))
                        {
                            item.JournalId = journal.JournalId;
                            //newJournals.Add(item);
                            queryResult = await _context.Database.ExecuteSqlAsync($"INSERT INTO GenJournalDetails (JournalId,AccountId,IsDebit,ClientId,CurrencyAmount,ExchangeRate,CashFlowId) VALUES ({item.JournalId} ,{item.AccountId},{item.IsDebit},{item.ClientId},{item.CurrencyAmount},{item.ExchangeRate},{item.CashFlowId})");
                        }
                    }

                    //var newJournals = journal.JournalDetail.Where(x => journalDetails.Any(y => x.JournalDetailId != y.JournalDetailId)).ToList();
                    //newJournals.ForEach(x => x.JournalId = journal.JournalId);
                    //await _context.GenJournalDetails.AddRangeAsync(newJournals);

                    //await _context.SaveChangesAsync();
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

    class GetJournalId
    {
        int JournalId { get; set; }
    }
}
