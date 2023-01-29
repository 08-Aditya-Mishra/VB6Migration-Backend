using System.ComponentModel.DataAnnotations;

namespace MigrationTask.Controllers.Dto
{
    public class TransferTransactionDto
    {
        [Required]
        public int SendersAccountNo { get; set; }

        [Required]
        public int ReceiversAccountNo { get; set; }
        [Required]
        public decimal TransactionAmount { get; set; }
    }
}