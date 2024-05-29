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

        [HttpGet("chats")]
        public async Task<IEnumerable<UserDto>> GetUserChats(CancellationToken cancellationToken)
        {
            IEnumerable<UserDto> chatUsers = new List<UserDto>();
            var uniqueUsers = await chatContext.Messages
                                    .AsNoTracking()
                                    .Where(m => m.FromId == UserId || m.ToId == UserId)
                                    .Select(m => new { From = m.FromId, To = m.ToId })
                                    .Distinct()
                                    .ToListAsync(cancellationToken);
            var uniqueUserIds = new HashSet<Guid>();
            uniqueUsers.ForEach(u =>
            {
                if (u.From != UserId)
                    uniqueUserIds.Add(u.From);
                if (u.To != UserId)
                    uniqueUserIds.Add(u.To);
            });
            if(uniqueUserIds.Count > 0)
            {
                chatUsers = await chatContext.Users
                                        .AsNoTracking()
                                        .Where(u => uniqueUserIds.Contains(u.Id))
                                        .Select(u => new UserDto(u.Id, u.Name, false))
                                        .ToListAsync(cancellationToken);
            }

            return chatUsers;
                                    
        }
    }
}
