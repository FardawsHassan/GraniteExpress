using GraniteExpress.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace GraniteExpress.Data
{
	public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : IdentityDbContext<ApplicationUser>(options)
	{
		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
            modelBuilder.Entity<Currency>(entity =>
            {
				entity.Property(e => e.DefaultValue)
					.HasDefaultValue(decimal.Parse("1"));
            });

            modelBuilder.Entity<Account>()
				.HasOne(p => p.AccountType)
				.WithMany(pn => pn.Accounts)
				.HasForeignKey(f => f.AccountTypeId)
				.OnDelete(DeleteBehavior.Cascade);

			modelBuilder.Entity<Account>()
				.HasOne(p => p.Currency)
				.WithMany(pn => pn.Accounts)
				.HasForeignKey(f => f.CurrencyId)
				.OnDelete(DeleteBehavior.Cascade);

			modelBuilder.Entity<JournalDetail>()
				.HasOne(p => p.Journal)
				.WithMany(pn => pn.JournalDetail)
				.HasForeignKey(f => f.JournalId)
				.OnDelete(DeleteBehavior.Cascade);

			//modelBuilder.Entity<JournalDetail>()
			//    .HasOne(p => p.Journal)
			//    .WithOne(pn => pn.JournalDetail)
			//    .HasForeignKey<JournalDetail>(f => f.JournalId)
			//    .OnDelete(DeleteBehavior.Cascade);

			modelBuilder.Entity<Journal>()
				.HasOne(p => p.Template)
				.WithMany(pn => pn.Journals)
				.HasForeignKey(f => f.TemplateId)
				.OnDelete(DeleteBehavior.Cascade);

			modelBuilder.Entity<Journal>()
				.HasOne(p => p.Client)
				.WithMany(pn => pn.Journals)
				.HasForeignKey(f => f.ClientId)
				.OnDelete(DeleteBehavior.Cascade);

			modelBuilder.Entity<JournalDetail>()
				.HasOne(p => p.Account)
				.WithMany(pn => pn.JournalDetail)
				.HasForeignKey(f => f.AccountId)
				.OnDelete(DeleteBehavior.Cascade);

			modelBuilder.Entity<Journal>()
				.HasOne(p => p.DocumentType)
				.WithMany(pn => pn.Journals)
				.HasForeignKey(f => f.DocumentTypeId)
				.OnDelete(DeleteBehavior.Cascade);

			modelBuilder.Entity<TemplateDetails>()
				.HasOne(p => p.Template)
				.WithMany(pn => pn.TemplateDetail)
				.HasForeignKey(f => f.TemplateId)
				.OnDelete(DeleteBehavior.Cascade);

			//modelBuilder.Entity<TemplateDetails>()
			//    .HasOne(p => p.Template)
			//    .WithOne(pn => pn.TemplateDetail)
			//    .HasForeignKey<Template>(f => f.TemplateId)
			//    .OnDelete(DeleteBehavior.Cascade);


			modelBuilder.Entity<TemplateDetails>()
				.HasOne(p => p.Account)
				.WithMany(pn => pn.TemplateDetails)
				.HasForeignKey(f => f.AccountId)
				.OnDelete(DeleteBehavior.Cascade);

			modelBuilder.Entity<Client>()
				.HasOne(p => p.clienType)
				.WithMany(pn => pn.Clients)
				.HasForeignKey(f => f.ClientTypeId)
				.OnDelete(DeleteBehavior.Cascade);


			base.OnModelCreating(modelBuilder);
		}


		public DbSet<Template> RefTemplate { get; set; }
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
