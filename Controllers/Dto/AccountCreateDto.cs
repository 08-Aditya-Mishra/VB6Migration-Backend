using System.ComponentModel.DataAnnotations;

namespace MigrationTask.Controllers.Dto
{
    public class AccountCreateDto
    {
        [Required]
        public string AccountName { get; set; }

        [Required]
        public decimal Amount { get; set; }

        [Required]
        public string Address { get; set; }

        [Required]
        public string PhoneNo { get; set; }

        [Required]
        public string Passport { get; set; }

        public DateTime DateOfOpened { get; set; }

        [Required]
        public string AccountType { get; set; }

        [Required]
        public string FingerprintID { get; set; }


    }

}