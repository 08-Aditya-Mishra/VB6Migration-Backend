using System.ComponentModel.DataAnnotations;

namespace MigrationTask.Controllers.Dto
{
    public class TransactionDto
    {
        [Required]
        public int AccountNo { get; set; }
        [Required]
        public decimal TransactionAmount { get; set; }
    }
}