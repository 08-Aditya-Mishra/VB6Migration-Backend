using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MigrationTask.Models
{
    public class Transaction
    {
        public Guid Id { get; set; }
        public int AccountNo { get; set; }
        public decimal TransactionAmount { get; set; }
        public string Purpose { get; set; }
        public DateTime Date { get; set; }
        public string TransactionType { get; set; }
    }

}
