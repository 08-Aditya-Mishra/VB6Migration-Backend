using MigrationTask.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MigrationTask.Models.Responses;
using MigrationTask.Services.TokenGenerators;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
using System.Security.Claims;
using MigrationTask.Models.Requests;
using MigrationTask.Services.TokenValidators;
using MigrationTask.Services.RefreshTokenRepositories;
using MigrationTask.Services.Authenticators;
using MigrationTask.Interfaces;
using MigrationTask.Controllers.Dto;
using System.ComponentModel.DataAnnotations;

namespace MigrationTask.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly ICreateAccountService _createAccountService;
        private readonly IReadAccountService _readAccountService;
        private readonly IReadSpecificAccountService _readSpecificAccountService;
        private readonly IUpdateAccountService _updateAccountService;
        private readonly IDeleteAccountService _deleteAccountService;

        public AccountController(ICreateAccountService createAccountService, IReadAccountService readAccountService,
                       IReadSpecificAccountService readSpecificAccountService, IUpdateAccountService updateAccountService, IDeleteAccountService deleteAccountService)
        {
            _createAccountService = createAccountService;
            _readAccountService = readAccountService;
            _readSpecificAccountService = readSpecificAccountService;
            _updateAccountService = updateAccountService;
            _deleteAccountService = deleteAccountService;
        }

        //GET /accounts: retrieve a list of accounts
        [HttpGet]
        public IActionResult GetAccounts()
        {
            try
            {
                var accounts = _readAccountService.GetAccounts();
                return Ok(accounts);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        //GET /accounts/{id}: retrieve a specific account by ID
        [HttpGet("{AccountNo}")]
        public List<Account> GetAccountByAccountNo(int AccountNo)
        {
            try
            {
                var account = _readSpecificAccountService.GetAccountByAccountNo(AccountNo);
                if (account == null)
                {
                    return null;
                }

                return account;
            }
            catch (Exception)
            {
                throw new Exception("An error occurred while trying to retrieve an account. Please try again later.");
            }
        }


        //POST api/accounts: create a new account
        [Authorize]
        [HttpPost]
        public IActionResult CreateAccount([FromBody] AccountCreateDto accountCreateDto)
        {
            try
            {
                var account = _createAccountService.CreateAccount(accountCreateDto);
                return Ok(account);
            }
            catch (ValidationException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                // Log the exception
                return BadRequest("Error creating account: " + ex.Message);
            }
        }

        //PUT api/accounts/{id}: update an existing account
        [HttpPut("{id}")]
        public IActionResult UpdateAccount(Guid id, [FromBody] AccountCreateDto accountCreateDto)
        {
            try
            {
                var existingAccount = _updateAccountService.UpdateAccount(id, accountCreateDto);  
                return Ok(existingAccount);
            }
            catch (Exception ex)
            {
                return BadRequest("Failed to update account. Error: " + ex.Message);
            }
        }

        //Delete api/accounts/{id}: update an existing account
        [HttpDelete("{id}")]
        public IActionResult DeleteAccount(Guid id)
        {
            try
            {
                _deleteAccountService.DeleteAccount(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest("DeleteAccount failed" + ex.GetType().Name);
            }

        }

    }
}