using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ZaptimeChatApp.Server.Data;
using ZaptimeChatApp.Shared.DTOs;

namespace ZaptimeChatApp.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : BaseController
    {
        private readonly ZaptimeChatDbContext chatContext;

        public UsersController(ZaptimeChatDbContext chatContext)
        {
            this.chatContext = chatContext;
        }

        [HttpGet]
        public async Task<IEnumerable<UserDto>> GetUsers()
        {
            return await chatContext.Users
                        .AsNoTracking()
                        .Where(u => u.Id != UserId)
                        .Select(u => new UserDto(u.Id, u.Name, false))
                        .ToListAsync();
        }
    }
}
