
using System.Net;
using MigrationTask.Controllers.Dto;
using MigrationTask.Interfaces;
using MigrationTask.Models;


namespace MigrationTask.Services
{
    public class UpdateAccountService : IUpdateAccountService
    {
        private readonly ApplicationDbContext _context;

        public UpdateAccountService(ApplicationDbContext context)
        {
            _context = context;
        }

        public Account UpdateAccount(Guid id, AccountCreateDto accountCreateDto)
        {
            try
            {
                var existingAccount = _context.accounts.Find(id);
                if (existingAccount == null)
                {
                    throw new Exception("Account not found");
                }
                existingAccount.AccountName = accountCreateDto.AccountName;
                existingAccount.Amount = accountCreateDto.Amount;
                existingAccount.Address = accountCreateDto.Address;
                existingAccount.PhoneNo = accountCreateDto.PhoneNo;
                existingAccount.Passport = accountCreateDto.Passport;
                existingAccount.AccountType = accountCreateDto.AccountType;
                existingAccount.FingerprintID = accountCreateDto.FingerprintID;

                _context.accounts.Update(existingAccount);
                _context.SaveChanges();
                return existingAccount;  
            }
            catch (Exception ex)
            {
                throw new CustomException("Error while updating account", ex, HttpStatusCode.BadRequest);
            }
        }
    }

}