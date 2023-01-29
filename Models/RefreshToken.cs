namespace MigrationTask.Models
{
    public class RefreshToken
    {
        public Guid Id { get; set; }
        public string Token { get; set; }
        public int AdminId { get; set; }    
    }
}
