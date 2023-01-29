
﻿using MigrationTask.Data;
using MigrationTask.Interfaces;
using MigrationTask.Models;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Mvc;
using System.Net;


namespace MigrationTask.Services
{
    public class AccountReadService : IReadAccountService
    {
        private readonly ApplicationDbContext _context;

        private readonly ILogger _logger;

        public AccountReadService(ApplicationDbContext context)
        {
            _context = context;
        }

        public List<Account> GetAccounts()
        {
            try
            {
                return _context.accounts.ToList();
            }
            catch(Exception ex)
            {
                throw new CustomException("Error while fetching accounts", ex, HttpStatusCode.BadRequest);
            }
        }
    }
}