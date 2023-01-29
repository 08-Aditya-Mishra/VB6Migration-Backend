
using MigrationTask;
using MigrationTask.Interfaces;
using MigrationTask.Models;
using System.Net;

namespace MigrationTask.Services
{
    public class DeleteAccountService : IDeleteAccountService
    {
        private readonly ApplicationDbContext _context;

        public DeleteAccountService(ApplicationDbContext context)
        {
            _context = context;
        }

        public void DeleteAccount(Guid id)
        {
            try
            {
                var account = _context.accounts.Find(id);
                if (account == null)
                {
                    throw new Exception("Account not found");
                }

               _context.accounts.Remove(account);
               _context.SaveChanges();

            }
            catch (Exception ex)
            {
                throw new CustomException("Error while deleting accounts", ex, HttpStatusCode.BadRequest);
            }
        }
    }

}