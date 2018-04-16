using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace HomeBudget2.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit https://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }

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
        public DbSet<SourceOMoney> SourcesOMoney { get; set; }

        public DbSet<DestinationOfMoney> DestinationsOfMoney { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<SubCategory> SubCategories { get; set; }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BankAccount>().Property(bankAcc => bankAcc.AccountName).IsRequired();

            modelBuilder.Entity<SubCategory>().Property(SubCat => SubCat.SubCategoryName).IsRequired();

            modelBuilder.Entity<Category>().Property(Cat => Cat.CategoryName).IsRequired();


            base.OnModelCreating(modelBuilder);
        }
    }
}