
using MigrationTask.Controllers.Dto;
using MigrationTask.Data;
using MigrationTask.Interfaces;
using MigrationTask.Models;
using System.Net;
using System.Text.RegularExpressions;
using System.Security.Cryptography;
using System.Text;

namespace MigrationTask.Services
{
    public class AccountCreateService : ICreateAccountService
    {
        private readonly ApplicationDbContext _context;

        public AccountCreateService(ApplicationDbContext context)
        {
            _context = context;
        }

        public Account CreateAccount(AccountCreateDto accountCreateDto)
        {
            try
            {
                // Perform validation/mapping of DTO here
                if (accountCreateDto == null)
                {
                    throw new ArgumentNullException("AccountCreateDto is null");
                }

                if (string.IsNullOrEmpty(accountCreateDto.AccountName) ||
                    string.IsNullOrEmpty(accountCreateDto.Address) ||
                    string.IsNullOrEmpty(accountCreateDto.PhoneNo) ||
                    string.IsNullOrEmpty(accountCreateDto.Passport) ||
                    string.IsNullOrEmpty(accountCreateDto.FingerprintID) ||
                    string.IsNullOrEmpty(accountCreateDto.AccountType))
                {
                    throw new ArgumentException("One or more required fields are missing");
                }

                if (accountCreateDto.Amount <= 0)
                {
                    throw new ArgumentException("Amount must be greater than 0");
                }

                if (!Regex.IsMatch(accountCreateDto.PhoneNo, @"^\+[0-9]{11,12}$"))
                {
                    throw new ArgumentException("Invalid phone number format");
                }


                if (_context.accounts.Any(a => a.FingerprintID == accountCreateDto.FingerprintID))
                {
                    throw new ArgumentException("Fingerprint ID is already in use");
                }


                /*if (!Regex.IsMatch(accountCreateDto.SMS, @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$"))
                {
                    throw new ArgumentException("Invalid email address format");
                }*/

                //ENcryption SHA-256
                string plainText = accountCreateDto.FingerprintID;
                string hashString;
                using (SHA256 sha256 = SHA256.Create())
                {
                    byte[] inputBytes = Encoding.UTF8.GetBytes(plainText);
                    byte[] hash = sha256.ComputeHash(inputBytes);
                    hashString = BitConverter.ToString(hash).Replace("-", "");
                    Console.WriteLine("Hash: " + hashString.ToLower());
                }

                var account = new Account
                {
                    AccountName = accountCreateDto.AccountName,
                    Amount = accountCreateDto.Amount,
                    Address = accountCreateDto.Address,
                    PhoneNo = accountCreateDto.PhoneNo,
                    Passport = accountCreateDto.Passport,
                    DateOfOpened = DateTime.Now,
                    AccountType = accountCreateDto.AccountType,
                    FingerprintID = hashString.ToLower(),
                    /*SMS = accountCreateDto.SMS,
                    SMSport = accountCreateDto.SMSport,*/
                };
                _context.accounts.Add(account);
                _context.SaveChanges();
                return account;
            }
            catch (Exception ex)
            {
                throw new CustomException("Error while creating account", ex, HttpStatusCode.BadRequest);
            }

        }

    }
}
