using System.ComponentModel.DataAnnotations;
namespace MigrationTask.Models
{
    public class AdminLogin
    {
        [Key]
        public int Id { get; set; }
        public string username { get; set; }
        public string password { get; set; }

    }
}