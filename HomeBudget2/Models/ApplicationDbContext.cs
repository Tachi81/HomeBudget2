using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;

namespace HomeBudget2.Models
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        public DbSet<BankAccount> BankAccounts { get; set; }
        public DbSet<FinancialOperation> FinancialOperations { get; set; }

        public DbSet<Category> Categories { get; set; }
        


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BankAccount>().Property(bankAcc => bankAcc.AccountName).IsRequired();

            // modelBuilder.Entity<FinancialOperation>().HasOptional(fo=>fo.SourceBankAccountId).WithRequired()

                                  

            modelBuilder.Entity<Category>().Property(Cat => Cat.CategoryName).IsRequired();


            base.OnModelCreating(modelBuilder);
        }
    }
}