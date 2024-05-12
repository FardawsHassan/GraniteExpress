using GraniteExpress.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace GraniteExpress.Data
{
	public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : IdentityDbContext<User>(options)
	{
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
                .OnDelete(DeleteBehavior.Cascade);

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
