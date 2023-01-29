
using MigrationTask.Controllers.Dto;
using MigrationTask.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace MigrationTask.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TransactionController : ControllerBase
    {
        private readonly ITransactionGetService _transactionGetService;
        private readonly ITransactionPostService _transactionPostService;

        public TransactionController(ITransactionGetService transactionService, ITransactionPostService transactionPostService)
        {
            _transactionGetService = transactionService;
            _transactionPostService = transactionPostService;
        }

        [HttpGet]
        public IActionResult GetTransactions()
        {
            try
            {
                var transactions = _transactionGetService.GetTransactions();
                return Ok(transactions);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{AccountNo}")]
        public IActionResult GetTransactionByAccountNo(int AccountNo)
        {
            try
            {
                var transactions = _transactionGetService.GetTransactionByAccountNo(AccountNo);
                if (transactions == null)
                {
                    return NotFound();
                }

                return Ok(transactions);
            }
            catch (Exception)
            {
                // Log the exception
                return StatusCode(500, "An error occurred while trying to retrieve the transaction. Please try again later.");
            }
        }

        [Authorize]
        [HttpPost("deposit")]
        public IActionResult Deposit([FromBody] TransactionDto transactionDto)
        {
            if (transactionDto.TransactionAmount <= 0)
            {
                return BadRequest("Invalid amount error");
            }
            try
            {
                var account = _transactionPostService.MakeDeposit(transactionDto);
                return Ok(account);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("withdraw")]
        public IActionResult Withdraw([FromBody] TransactionDto transactionDto)
        {
            try
            {
                var account = _transactionPostService.MakeWithdraw(transactionDto);
                return Ok(account);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception e)
            {
                // Log the exception
                return BadRequest(e.Message);
            }
        }

        [HttpPost("transfer")]
        public IActionResult Transfer([FromBody] TransferTransactionDto transferTransactionDto)
        {
            try
            {
                _transactionPostService.MakeTransfer(transferTransactionDto);
                return Ok();
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception)
            {
                // Log the exception
                return StatusCode(500, "An error occurred while trying to transfer. Please try again later.");
            }
        }
    }

}