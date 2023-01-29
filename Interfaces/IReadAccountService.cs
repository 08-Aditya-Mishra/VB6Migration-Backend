using MigrationTask.Models;

namespace MigrationTask.Interfaces
{
    public interface IReadAccountService
    {
       List<Account> GetAccounts();
    }
}