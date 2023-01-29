using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using MigrationTask.Models;
using System.ComponentModel.DataAnnotations;

namespace MigrationTask.Models.Requests
{
    public class RefreshRequest
    {
        [Required]
        public string RefreshToken { get; set; }
    }
}