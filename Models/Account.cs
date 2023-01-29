using System.ComponentModel.DataAnnotations;

namespace MigrationTask.Models
{
    public class Account
    {
        public Guid ID { get; set; }
        public int AccountNo { get; set; }
        public string AccountName { get; set; }
        public decimal Amount { get; set; }
        public string Address { get; set; }
        public string PhoneNo { get; set; }
        public string Passport { get; set; }
        public DateTime DateOfOpened { get; set; }
        public string AccountType { get; set; }
        public string FingerprintID { get; set; }
    }

}
