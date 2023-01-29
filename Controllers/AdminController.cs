using MigrationTask.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MigrationTask.Models.Responses;
using MigrationTask.Services.TokenGenerators;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
using System.Security.Claims;
using MigrationTask.Models.Requests;
using MigrationTask.Services.TokenValidators;
using MigrationTask.Services.RefreshTokenRepositories;
using MigrationTask.Services.Authenticators;

namespace MigrationTask.Controllers
{
    
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly ApplicationDbContext _Context;
        private readonly Authenticator _authenticator;
        private readonly RefreshTokenValidator _refreshTokenValidator;
        private readonly IRefreshTokenRepository _refreshTokenRepository;

        public AdminController(IRefreshTokenRepository refreshTokenRepository,ApplicationDbContext context, Authenticator authenticator, RefreshTokenValidator refreshTokenValidator)
        {
            _Context = context;
            _authenticator = authenticator;
            _refreshTokenValidator = refreshTokenValidator;
            _refreshTokenRepository = refreshTokenRepository;
        }


        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] AdminLogin value)
        {
            if(!ModelState.IsValid){
                return BadRequest();
            }
            var admin = await _Context.admins.FirstOrDefaultAsync(u => u.username == value.username);
            if(admin != null){
                var passwordMatch = admin.password == value.password;
                if(!passwordMatch){
                    return Unauthorized();
                }
                AuthenticatedUserResponse response = await _authenticator.Authenticate(admin);
                return Ok(response);
            }else{
                return Unauthorized();
            }
            // var userName = await _Context.admins.AnyAsync(u => u.username == value.username);
            // if(userName == null)
            // {
            //     return Unauthorized();
            // }
            // var passWord = await _Context.admins.AnyAsync(u => u.password == value.password);
            // if(passWord == null)
            // {
            //     return Unauthorized();
            // }
            
            
        }

        [HttpPost("refresh")]
        public async Task<IActionResult> Refresh([FromBody] RefreshRequest refreshRequest)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest();
            }
            bool isValidRefreshToken = _refreshTokenValidator.Validate(refreshRequest.RefreshToken);
            if(!isValidRefreshToken)
            {
                return BadRequest();
            }
            RefreshToken refreshTokenDTO = await _refreshTokenRepository.GetByToken(refreshRequest.RefreshToken);
            if(refreshTokenDTO == null)
            {
                return NotFound();
            }
            await _refreshTokenRepository.Delete(refreshTokenDTO.Id);
            var admin = await _Context.admins.FirstOrDefaultAsync(u => u.Id.Equals(refreshTokenDTO.AdminId));
            if(admin == null)
            {
                return NotFound();
            }
            AuthenticatedUserResponse response = await _authenticator.Authenticate(admin);
            return Ok(response);

        }
        
        [Authorize]
        [HttpDelete("logout")]
        public async Task<IActionResult> Logout()
        {
            string id = HttpContext.User.FindFirstValue("id");
            if(!int.TryParse(id,out int AdminId))
            {
                return Unauthorized();
            }
            await _refreshTokenRepository.DeleteAll(AdminId);
            return NoContent();
        }
    }
}