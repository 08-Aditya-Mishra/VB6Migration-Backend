using MigrationTask.Controllers.Dto;
using MigrationTask.Models;

namespace MigrationTask.Interfaces
{
    public interface ICreateAccountService
    {
        Account CreateAccount(AccountCreateDto accountCreateDto);
    }

}