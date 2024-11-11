using AutoMapper;
using GraniteExpress.Data;
using GraniteExpress.DtoModels;
using GraniteExpress.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace GraniteExpress.Services
{
    public interface IJournalService
    {
        Task<List<JournalDto>> GetJournals();
        Task<JournalDto> GetJournalById(int journalId);
        Task<List<JournalView>> GetJournalViews();
        Task<bool> SaveJournal(JournalDto journal);
        Task<bool> DeleteJournal(int journalId);
        List<SelectJournalView> JournalSelect(DateTime? start, DateTime? end);
    }

    public class JournalService : IJournalService
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<JournalService> _logger;
        private readonly IMapper _mapper;

        public JournalService(ApplicationDbContext context, ILogger<JournalService> logger, IMapper mapper)
        {
            _context = context;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<List<JournalDto>> GetJournals()
        {
            try
            {
                var journals = await _context.GenJournal.Include(x => x.JournalDetail).ToListAsync();
                return _mapper.Map<List<JournalDto>>(journals);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Method->GetJournals Error->{ex.Message}");
                return null;
            }
        }

        public async Task<JournalDto> GetJournalById(int journalId)
        {
            try
            {
                var journal = await _context.GenJournal.Where(j => j.JournalId == journalId).Include(x => x.JournalDetail).FirstOrDefaultAsync();
                if (journal is null) return null;

                //journal.JournalDetail.RemoveAll(x => x.JournalId == 0);
                return _mapper.Map<JournalDto>(journal);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Method->GetJounalById Error->{ex.Message}");
                return null;
            }
        }

        public async Task<bool> DeleteJournal(int journalId)
        {
            try
            {
                var result = await _context.Database.ExecuteSqlAsync($"UPDATE GenJournal SET IsDelete = 1 WHERE JournalId = {journalId}");
                return result > 0 ? true : false;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Method->DeleteJournal Error->{ex.Message}");
                return false;
            }

        }

        public async Task<List<JournalView>> GetJournalViews()
        {
            try
            {
                var journalViews = from journal in _context.GenJournal
                                   where journal.IsDelete == false
                                   join journalDetails in _context.GenJournalDetails on journal.JournalId equals journalDetails.JournalId
                                   join account in _context.RefAccount on journalDetails.AccountId equals account.AccountId
                                   join client in _context.RefClient on journal.ClientId equals client.ClientId
                                   join documentType in _context.RefDocumentType on journal.DocumentTypeId equals documentType.DocumentTypeId
                                   select new JournalView
                                   {
                                       DocumentTypeId = journal.DocumentTypeId,
                                       DocumentNo = journal.DocumentNo,
                                       JournalId = journal.JournalId,
                                       JournalDescription = journal.JournalDescription,
                                       DocumentDate = journal.DocumentDate,
                                       AccountCode = account.AccountCode,
                                       IsActive = account.IsActive,
                                       ClientFirstName = client.ClientFirstName,
                                       ClientLastName = client.ClientLastName,
                                       IsDebit = journalDetails.IsDebit,
                                       CurrencyAmount = journalDetails.CurrencyAmount,
                                       ExchangeRate = journalDetails.ExchangeRate,
                                       DocumentName = documentType.DocumentName
                                   };
                return journalViews.ToList();

            }
            catch (Exception ex)
            {
                _logger.LogError($"Method->GetJournalViews Error->{ex.Message}");
                return null;
            }
        }

        public async Task<bool> SaveJournal(JournalDto journal)
        {
            try
            {
                var result = _context.GenJournal.Where(x => x.JournalId == journal.JournalId).FirstOrDefault();
                if (result is null)
                {
                    List<JournalDto> list;
                    string sql = string.Empty;
                    if (journal.TemplateId is null)
                    {
                        sql = "EXEC AddJournal NULL, @DocumentDate,@JournalDescription,@DocumentTypeId,@ClientId,@DocumentNo,@UserId";
                    }
                    else
                    {
                        sql = "EXEC AddJournal @TemplateId, @DocumentDate,@JournalDescription,@DocumentTypeId,@ClientId,@DocumentNo,@UserId";
                    }

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
                    };
                    if (journal.TemplateId is not null)
                    {
                        journalParams.Add(new SqlParameter { ParameterName = "@TemplateId", Value = journal.TemplateId });
                    }

                    var responese = _context.Database.SqlQueryRaw<JournalDto>(sql, journalParams.ToArray()).ToList();


                    var journalDetails = journal.JournalDetail;
                    foreach (var item in journalDetails)
                    {
                        item.JournalId = responese.FirstOrDefault().JournalId;
                        var queryResult = await _context.Database.ExecuteSqlAsync($"INSERT INTO GenJournalDetails (JournalId,AccountId,IsDebit,ClientId,CurrencyAmount,ExchangeRate,CashFlowId) VALUES ({item.JournalId} ,{item.AccountId},{item.IsDebit},{item.ClientId},{item.CurrencyAmount},{item.ExchangeRate},{item.CashFlowId})");
                    }
                    return true;
                }
                else
                {
                    var queryResult = await _context.Database.ExecuteSqlAsync($"UPDATE GenJournal SET IsDelete = {journal.IsDelete}, JournalDescription = {journal.JournalDescription}, DocumentTypeId = {journal.DocumentTypeId}, ClientId = {journal.ClientId}, DocumentNo = {journal.DocumentNo}, TemplateId = {journal.TemplateId} WHERE JournalId = {journal.JournalId}");
                    if (queryResult == 0) return false;

                    var journalDetails = _context.GenJournalDetails.Where(x => x.JournalId == journal.JournalId);
                    foreach (var item in journalDetails)
                    {
                        var updateJournal = journal.JournalDetail.Where(x => x.JournalDetailId == item.JournalDetailId).FirstOrDefault();
                        if (updateJournal is not null)
                        {
                            queryResult = await _context.Database.ExecuteSqlAsync($"UPDATE GenJournalDetails SET AccountId = {updateJournal.AccountId}, CashFlowId = {updateJournal.CashFlowId}, ClientId = {updateJournal.ClientId}, IsDebit = {updateJournal.IsDebit}, ExchangeRate = {updateJournal.ExchangeRate}, CurrencyAmount = {updateJournal.CurrencyAmount} WHERE JournalDetailId = {updateJournal.JournalDetailId}");
                        }
                        else
                        {
                            queryResult = await _context.Database.ExecuteSqlAsync($"DELETE FROM GenJournalDetails WHERE JournalDetailId = {item.JournalDetailId}");
                        }
                    }

                    List<JournalDetail> newJournals = new();
                    foreach (var item in journal.JournalDetail)
                    {
                        if (!journalDetails.Any(x => x.JournalDetailId == item.JournalDetailId))
                        {
                            item.JournalId = journal.JournalId;
                            queryResult = await _context.Database.ExecuteSqlAsync($"INSERT INTO GenJournalDetails (JournalId,AccountId,IsDebit,ClientId,CurrencyAmount,ExchangeRate,CashFlowId) VALUES ({item.JournalId} ,{item.AccountId},{item.IsDebit},{item.ClientId},{item.CurrencyAmount},{item.ExchangeRate},{item.CashFlowId})");
                        }
                    }
                    return true;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Method->SaveJournal Error->{ex.Message}");
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
                    new SqlParameter { ParameterName = "@edate", Value = end },
                    new SqlParameter { ParameterName = "@bdate", Value = start }
                };

                return _context.Database.SqlQueryRaw<SelectJournalView>(sql, parms.ToArray()).ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Method->JournalSelect Error->{ex.Message}");
                return null;
            }
        }
    }
}
