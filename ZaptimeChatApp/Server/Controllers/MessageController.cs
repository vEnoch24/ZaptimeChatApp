using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using ZaptimeChatApp.Server.Data;
using ZaptimeChatApp.Server.Data.Models;
using ZaptimeChatApp.Server.Hubs;
using ZaptimeChatApp.Shared;
using ZaptimeChatApp.Shared.DTOs;

namespace ZaptimeChatApp.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MessageController : BaseController
    {
        private readonly ZaptimeChatDbContext _chatContext;
        private readonly IHubContext<ZaptimeChatHub, IZaptimeChatHubClient> _hubContext;

        public MessageController(ZaptimeChatDbContext chatContext, IHubContext<ZaptimeChatHub, IZaptimeChatHubClient> hubContext)
        {
            _chatContext = chatContext;
            _hubContext = hubContext;
        }

        [HttpPost("send-message")]
        public async Task<IActionResult> SendMessage(MessageSendDto messageDto, CancellationToken cancellationToken)
        {
            if (messageDto.ToUserId == Guid.Empty || string.IsNullOrWhiteSpace(messageDto.Message))
                return BadRequest();

            var message = new Message
            {
                FromId = base.UserId,
                ToId = messageDto.ToUserId,
                Content = messageDto.Message,
                SentOn = DateTime.Now
            };

            await _chatContext.Messages.AddAsync(message, cancellationToken);
            if(await _chatContext.SaveChangesAsync(cancellationToken) > 0)
            {
                var responseMessageDto = new MessageDto(message.ToId, message.FromId, message.Content, message.SentOn);
                await _hubContext.Clients.User(messageDto.ToUserId.ToString())
                        .MessageReceived(responseMessageDto);
                return Ok();
            }
            else
            {
                return StatusCode(500, "unable to send message");
            }
        }

        [HttpGet("{otherUserId:Guid}")]
        public async Task<IEnumerable<MessageDto>> GetMessages(Guid otherUserId, CancellationToken cancellationToken)
        {
            var messages = await _chatContext.Messages.AsNoTracking()
                            .Where( m => 
                            (m.FromId == otherUserId && m.ToId == UserId) || (m.ToId == otherUserId && m.FromId == UserId)
                            )
                            .Select(m => new MessageDto(m.ToId, m.FromId, m.Content, m.SentOn))
                            .ToListAsync(cancellationToken);

            return messages;
        }
    }
}
