using MigrationTask.Data;
using MigrationTask.Interfaces;
using MigrationTask.Models;
using System.Net;



namespace MigrationTask.Services
{
    public class SpecificAccountReadService : IReadSpecificAccountService
    {
        private readonly ApplicationDbContext _context;

        public SpecificAccountReadService(ApplicationDbContext context)
        {
            _context = context;
        }

        public List<Account> GetAccountByAccountNo(int accountNo)
        {
            try
            {
                var account = _context.accounts.Where(t => t.AccountNo == accountNo).ToList();
                if (account == null)
                {
                    throw new Exception("Account not found");
                }
                else
                {
                    return account;
                }
            }
            catch (Exception ex)
            {
                throw new CustomException("Error while fetching account", ex, HttpStatusCode.BadRequest);
            }
        }
    }
}