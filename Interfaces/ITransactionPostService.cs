using MigrationTask.Controllers.Dto;
using MigrationTask.Models;

namespace MigrationTask.Interfaces
{
    public interface ITransactionPostService
    {
        Transaction MakeDeposit(TransactionDto transactionDto);
        Transaction MakeWithdraw(TransactionDto transactionDto);
        (Transaction fromTransaction, Transaction toTransaction) MakeTransfer(TransferTransactionDto transferTransactionDto);
    }

}