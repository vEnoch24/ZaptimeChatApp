using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using ZaptimeChatApp.Server.Data;
using ZaptimeChatApp.Server.Data.Models;
using ZaptimeChatApp.Server.Hubs;
using ZaptimeChatApp.Shared;
using ZaptimeChatApp.Shared.DTOs;

namespace ZaptimeChatApp.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly ZaptimeChatDbContext _dbContext;
        private readonly TokenService _tokenService;
        private readonly IHubContext<ZaptimeChatHub, IZaptimeChatHubClient> _hubContext;

        public AccountController(ZaptimeChatDbContext dbContext, TokenService tokenService, IHubContext<ZaptimeChatHub, IZaptimeChatHubClient> hubContext)
        {
            _dbContext = dbContext;
            _tokenService = tokenService;
            _hubContext = hubContext;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterDto dto, CancellationToken cancellationToken)
        {
            var usernameExist = await _dbContext.Users.AsNoTracking().AnyAsync( u => u.UserName == dto.UserName, cancellationToken);

            if(usernameExist)
            {
                return BadRequest($"{nameof(dto.UserName)} already exist");
            }

            CreatePasswordHash(dto.Password, out byte[] passwordHash, out byte[] passwordSalt);

            var user = new User
            {
                UserName = dto.UserName,
                Name = dto.Name,
                passwordhash = passwordHash,
                passwordSalt = passwordSalt,
            };

            await _dbContext.Users.AddAsync(user, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);

            await _hubContext.Clients.All.UserConnected(new UserDto(user.Id, user.Name));

            return Ok(GenerateToken(user));
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto dto, CancellationToken cancellationToken)
        {
            var user = await _dbContext.Users.FirstOrDefaultAsync(u => u.UserName == dto.UserName);

            if(user == null)
            {
                return BadRequest("User not found");
            }
            if (!VerifyPasswordHash(dto.Password, user.passwordhash, user.passwordSalt))
            {
                return BadRequest("Wrong Password!");
            }

            return Ok(GenerateToken(user));
        }        

        private AuthResponseDto GenerateToken(User user)
        {
            var token = _tokenService.CreateToken(user);
            return new AuthResponseDto(new UserDto(user.Id, user.Name), token);
        }

        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }
        private bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512(passwordSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                return computedHash.SequenceEqual(passwordHash);
            }
        }

    }
}
