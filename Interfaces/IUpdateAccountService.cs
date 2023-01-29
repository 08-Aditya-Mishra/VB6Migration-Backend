using MigrationTask.Controllers.Dto;
using MigrationTask.Models;

namespace MigrationTask.Interfaces
{
    public interface IUpdateAccountService
    {
        Account UpdateAccount(Guid id, AccountCreateDto accountCreateDto);
    }

}