using MigrationTask.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace MigrationTask.Data
{
    public static class ModelBuilderExtensions
    {
        public static void ConfigureAccounts(this ModelBuilder modelBuilder)
        {
            //Transacation Amount set to decimal 
            modelBuilder.Entity<Transaction>()
                .Property(t => t.TransactionAmount)
                .HasColumnType("decimal(18,2)");

            //Account Amount set to decimal 
            modelBuilder.Entity<Account>()
                .Property(t => t.Amount)
                .HasColumnType("decimal(18,2)");

            //Account - AccountNo - unique
            modelBuilder.Entity<Account>()
                .HasAlternateKey(a => a.AccountNo)
                .HasName("AccountNo_Unique");

            //Account Auto-generated(identity column
            modelBuilder.Entity<Account>()
                .Property(a => a.AccountNo)
                .ValueGeneratedOnAdd();

            modelBuilder.Ignore <IdentityUserLogin<string>>();
            modelBuilder.Ignore <IdentityUserRole<string>>();
            modelBuilder.Ignore<IdentityUserClaim<string>>();
            modelBuilder.Ignore<IdentityUserToken<string>>();
            modelBuilder.Ignore<IdentityUser<string>>();

        }
    }
}
