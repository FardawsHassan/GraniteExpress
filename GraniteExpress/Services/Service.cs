//using GraniteExpress.Data;
//using GraniteExpress.Migrations;
//using GraniteExpress.Models;
//using Microsoft.AspNetCore.Identity;
//using MudBlazor.Extensions;

//namespace GraniteExpress.Services
//{
//    public interface IService
//    {
//        Task<ApplicationUser> GetUserIdByEmail(string email);
//        Task<List<Template>> GetTemplates();
//        Task<List<Journal>> GetJournals();
//        Task<List<DocumentType>> GetDocumentTypes();
//        Task<List<Currency>> GetCurrencies();
//        Task<List<AccountType>> GetAccountTypes();
//        Task<List<TemplateDetails>> GetTemplateDetails();
//        Task<List<Account>> GetAccounts();
//        Task<Account> AddAccount(Account account);
//        Task<Journal> AddJournal(Journal journal);
//        Task<TemplateDetails> AddTemplateDetails(TemplateDetails templateDetails);
//    }

//    public class Service : IService
//    {
//        private readonly ApplicationDbContext _context;

//        public Service(ApplicationDbContext context)
//        {
//            _context = context;
//        }

//        public async Task<ApplicationUser> GetUserIdByEmail(string email)
//        {
//            try
//            {
//                return _context.Users.Where(x => x.UserName == email).FirstOrDefault();
                
//            }
//            catch (Exception)
//            {

//                return null;
//            }
//        }
        
//        public async Task<List<Template>> GetTemplates()
//        {
//            return _context.RefTemplate.ToList();
//        }
        
//        public async Task<List<TemplateDetails>> GetTemplateDetails()
//        {
//            return  _context.RefTemplateDetails.ToList();
//        }
//        public async Task<List<DocumentType>> GetDocumentTypes()
//        {
//            return _context.RefDocumentType.ToList();
//        }

//        public async Task<List<Journal>> GetJournals()
//        {
//            return _context.GenJournal.ToList();
//        }

//        public async Task<List<Currency>> GetCurrencies()
//        {
//            return _context.RefCurrency.ToList();
//        }

//        public async Task<List<AccountType>> GetAccountTypes()
//        {
//            return _context.RefAccountType.ToList();
//        }
        
//        public async Task<List<Account>> GetAccounts()
//        {
//            return _context.RefAccount.ToList();
//        }
        
//        public async Task<Account> AddAccount(Account account)
//        {
//            try
//            {
//                var response =  await _context.RefAccount.AddAsync(account);
//                await _context.SaveChangesAsync();

//                account.AccountId = response.Entity.AccountId;
//                return account;
//            }
//            catch (Exception)
//            {
//                return null;
//            }
//        }

//        public async Task<TemplateDetails> AddTemplateDetails(TemplateDetails templateDetails)
//        {
//            try
//            {
//                var response =  await _context.RefTemplateDetails.AddAsync(templateDetails);
//                await _context.SaveChangesAsync();

//                templateDetails.TemplateDetailsId = response.Entity.TemplateDetailsId;
//                return templateDetails;
//            }
//            catch (Exception)
//            {
//                return null;
//            }
//        }
        
//        public async Task<Journal> AddJournal(Journal journal)
//        {
//            try
//            {
//                journal.JournalId = 0;
//                var response =  await _context.GenJournal.AddAsync(journal);
//                await _context.SaveChangesAsync();

//                journal.JournalId = response.Entity.JournalId;
//                return journal;
//            }
//            catch (Exception)
//            {
//                return null;
//            }
//        }
//    }
//}
