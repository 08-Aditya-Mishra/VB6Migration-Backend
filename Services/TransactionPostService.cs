
using MigrationTask.Controllers.Dto;
using MigrationTask.Interfaces;
using MigrationTask.Models;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace MigrationTask.Services
{
    public class TransactionPostService : ITransactionPostService
    {
        private readonly ApplicationDbContext _context;

        public TransactionPostService(ApplicationDbContext context)
        {
            _context = context;
        }

        public Transaction MakeDeposit(TransactionDto transactionDto)
        {
            try
            {
                // check whether the accountNo is present in Account or not 
                var account = _context.accounts.FirstOrDefault(a => a.AccountNo == transactionDto.AccountNo);
                if (account == null)
                {
                    throw new Exception("Account not found");
                }

                var transaction = new Transaction
                {
                    AccountNo = transactionDto.AccountNo,
                    Date = DateTime.Now,
                    Purpose = "Deposit",
                    TransactionAmount = transactionDto.TransactionAmount,
                    TransactionType = "Credit"
                };

                _context.transactions.Add(transaction);
                account.Amount += transactionDto.TransactionAmount;
                _context.SaveChanges();
                return transaction;
            }
            catch (Exception ex)
            {
                throw new CustomException("Error while depositing amount in account", ex, HttpStatusCode.BadRequest);

            }

        }

        public Transaction MakeWithdraw(TransactionDto transactionDto)
        {
            try
            {
                // check whether the accountNo is present in Account or not 
                var account = _context.accounts.FirstOrDefault(a => a.AccountNo == transactionDto.AccountNo);
                if (account == null)
                {

                    throw new Exception("Account not found");
                }

                if (account.Amount < transactionDto.TransactionAmount)
                {
                    throw new Exception("Insufficient balance");
                }

                var transaction = new Transaction
                {
                    AccountNo = transactionDto.AccountNo,
                    Date = DateTime.Now,
                    Purpose = "Withdraw",
                    TransactionAmount = transactionDto.TransactionAmount,
                    TransactionType = "Debit"
                };

                _context.transactions.Add(transaction);
                account.Amount -= transactionDto.TransactionAmount;
                _context.SaveChanges();
                return transaction;
            }
            catch (Exception ex)
            {
                throw new CustomException("Error while withdrawing amount from account", ex, HttpStatusCode.BadRequest);
            }

        }

        public (Transaction fromTransaction, Transaction toTransaction) MakeTransfer(TransferTransactionDto transferTransactionDto)
        {
            try
            {
                //validate input
                if (transferTransactionDto.TransactionAmount <= 0)
                {
                    throw new Exception("Invalid amount");
                }

                // check if the fromAccount exists
                var fromAccount = _context.accounts.FirstOrDefault(a => a.AccountNo == transferTransactionDto.SendersAccountNo);
                if (fromAccount == null)
                {
                    throw new Exception("Sender's account not found");
                }

                // check if the toAccount exists
                var toAccount = _context.accounts.FirstOrDefault(a => a.AccountNo == transferTransactionDto.ReceiversAccountNo);
                if (toAccount == null)
                {
                    throw new Exception("Receivers account not found");
                }

                //Check if both accounts are same
                if (transferTransactionDto.SendersAccountNo == transferTransactionDto.ReceiversAccountNo)
                {
                    throw new Exception("Both accounts are same");
                }

                // check if the fromAccount has sufficient balance
                if (fromAccount.Amount < transferTransactionDto.TransactionAmount)
                {
                    throw new Exception("Insufficient balance in Sender's account");
                }

                // create transaction for fromAccount
                var fromTransaction = new Transaction
                {
                    AccountNo = transferTransactionDto.SendersAccountNo,
                    Date = DateTime.Now,
                    Purpose = "Transfer",
                    TransactionAmount = transferTransactionDto.TransactionAmount,
                    TransactionType = "Debit"
                };

                // create transaction for toAccount
                var toTransaction = new Transaction
                {
                    AccountNo = transferTransactionDto.ReceiversAccountNo,
                    Date = DateTime.Now,
                    Purpose = "Transfer",
                    TransactionAmount = transferTransactionDto.TransactionAmount,
                    TransactionType = "Credit"
                };

                // perform transfer
                fromAccount.Amount -= transferTransactionDto.TransactionAmount;
                toAccount.Amount += transferTransactionDto.TransactionAmount;
                _context.transactions.Add(fromTransaction);
                _context.transactions.Add(toTransaction);
                _context.SaveChanges();

                return (fromTransaction, toTransaction);
            }
            catch (Exception ex)
            {
                throw new CustomException("Error while transfering amount from account", ex, HttpStatusCode.BadRequest);
            }
        }

    }

}

