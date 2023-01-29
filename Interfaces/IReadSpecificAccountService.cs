using MigrationTask.Models;

namespace MigrationTask.Interfaces
{
    public interface IReadSpecificAccountService
    {
        List<Account> GetAccountByAccountNo(int accountNo);
    }

}