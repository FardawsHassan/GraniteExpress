using Blazored.LocalStorage;
using GraniteExpress.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using TeamWorkServer.Services;

namespace GraniteExpress.Data
{
	public class ApplicationDbContext : IdentityDbContext<User>
    {
        private readonly IDatabaseResolver _databaseResolver;

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, IDatabaseResolver databaseResolver) : base(options)
        {
            _databaseResolver = databaseResolver;
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
            modelBuilder.Entity<Currency>(entity =>
            {
				entity.Property(e => e.DefaultValue)
					.HasDefaultValue(decimal.Parse("1"));
            });

            modelBuilder.Entity<User>()
                    .ToTable(nameof(User));

            modelBuilder.Entity<Account>()
                .HasOne(p => p.AccountType)
                .WithMany(pn => pn.Accounts)
                .HasForeignKey(f => f.AccountTypeId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Account>()
                .HasOne(p => p.Currency)
                .WithMany(pn => pn.Accounts)
                .HasForeignKey(f => f.CurrencyId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<JournalDetail>()
                .HasOne(p => p.Journal)
                .WithMany(pn => pn.JournalDetail)
                .HasForeignKey(f => f.JournalId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<JournalDetail>()
                .HasOne(p => p.CashFlow)
                .WithMany(pn => pn.JournalDetails)
                .HasForeignKey(f => f.CashFlowId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Journal>()
                .HasOne(p => p.Template)
                .WithMany(pn => pn.Journals)
                .HasForeignKey(f => f.TemplateId)
                .OnDelete(DeleteBehavior.Restrict);


            modelBuilder.Entity<Journal>()
                .HasOne(p => p.User)
                .WithMany(pn => pn.Journals)
                .HasForeignKey(f => f.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Journal>()
                .HasOne(p => p.Client)
                .WithMany(pn => pn.Journals)
                .HasForeignKey(f => f.ClientId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<JournalDetail>()
                .HasOne(p => p.Account)
                .WithMany(pn => pn.JournalDetail)
                .HasForeignKey(f => f.AccountId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Journal>()
                .HasOne(p => p.DocumentType)
                .WithMany(pn => pn.Journals)
                .HasForeignKey(f => f.DocumentTypeId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<TemplateDetails>()
                .HasOne(p => p.Template)
                .WithMany(pn => pn.TemplateDetail)
                .HasForeignKey(f => f.TemplateId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<TemplateDetails>()
                .HasOne(p => p.Account)
                .WithMany(pn => pn.TemplateDetails)
                .HasForeignKey(f => f.AccountId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Client>()
                .HasOne(p => p.clienType)
                .WithMany(pn => pn.Clients)
                .HasForeignKey(f => f.ClientTypeId)
                .OnDelete(DeleteBehavior.Restrict);

            base.OnModelCreating(modelBuilder);
        }


        protected override async void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //try
            //{
                //ILocalStorageService localStorage = new();
                var databaseConnectionString = _databaseResolver.GetConnectionString();
                //var tenantConnectionString = await localStorage.GetItemAsync<string>(key: "Database");
                if (!string.IsNullOrEmpty(databaseConnectionString))
                {
                    optionsBuilder.UseSqlServer($"Server=(localdb)\\Shuvro;Database={databaseConnectionString};Trusted_Connection=True;MultipleActiveResultSets=true;TrustServerCertificate=True");
                }
            //}
            //catch (Exception)
            //{
            //    optionsBuilder.UseSqlServer($"Server=(localdb)\\Shuvro;Database=GraniteExpress5;Trusted_Connection=True;MultipleActiveResultSets=true;TrustServerCertificate=True");
            //}
        }


        public DbSet<Template> RefTemplate { get; set; }
        public DbSet<CashFlow> RefCashFlow { get; set; }
        public DbSet<Client> RefClient { get; set; }
        public DbSet<TemplateDetails> RefTemplateDetails { get; set; }
        public DbSet<Journal> GenJournal { get; set; }
        public DbSet<JournalDetail> GenJournalDetails { get; set; }
        public DbSet<Account> RefAccount { get; set; }
        public DbSet<AccountType> RefAccountType { get; set; }
        public DbSet<Currency> RefCurrency { get; set; }
        public DbSet<DocumentType> RefDocumentType { get; set; }

    }
}
