using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using MigrationTask.Data;

namespace MigrationTask.Models
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }
        public DbSet<Account> accounts { get; set; }
        public DbSet<Transaction> transactions { get; set; }
        public DbSet<AdminLogin> admins { get; set; }
        //public DbSet<RefreshToken> refreshTokens { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //calling COnfigureAccounts in ModelBuilderExtensions 
            modelBuilder.ConfigureAccounts();
        }
    }
}