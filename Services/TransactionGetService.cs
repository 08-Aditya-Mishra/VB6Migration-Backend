using MigrationTask.Interfaces;
using MigrationTask.Models;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace MigrationTask.Services
{
    public class TransactionGetService : ITransactionGetService
    {
        private readonly ApplicationDbContext _context;

        public TransactionGetService(ApplicationDbContext context)
        {
            _context = context;
        }

        public List<Transaction> GetTransactions()
        {
            try
            {
                return _context.transactions.ToList();
            }
            catch (Exception ex)
            {
                throw new CustomException("Error while fetching transactions", ex, HttpStatusCode.BadRequest);
            }
        }

        public List<Transaction> GetTransactionByAccountNo(int AccountNo)
        {
            try
            {
                return _context.transactions.Where(t => t.AccountNo == AccountNo).ToList();
            }
            catch(Exception ex)
            {
                throw new CustomException("Error while fetching transaction", ex, HttpStatusCode.BadRequest);
            }
        }
 

    }

}