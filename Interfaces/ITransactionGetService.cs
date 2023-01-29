using MigrationTask.Models;

namespace MigrationTask.Interfaces
{
    public interface ITransactionGetService
    {
        public List<Transaction> GetTransactions();
        public List<Transaction> GetTransactionByAccountNo(int AccountNo);
    }

}