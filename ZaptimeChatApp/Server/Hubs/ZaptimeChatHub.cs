using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using ZaptimeChatApp.Shared;
using ZaptimeChatApp.Shared.DTOs;

namespace ZaptimeChatApp.Server.Hubs
{
    [Authorize]
    public class ZaptimeChatHub : Hub<IZaptimeChatHubClient>, IZaptimeChatHubServer
    {
        //private static readonly ICollection<string> connectedusers = new HashSet<string>(StringComparer.OrdinalIgnoreCase);

        private static IDictionary<Guid, UserDto> onlineUsers = new Dictionary<Guid, UserDto>();
        public ZaptimeChatHub()
        {

        }

        public override Task OnConnectedAsync()
        {
            return base.OnConnectedAsync();
        }

        public async Task SetUserOnline(UserDto user)
        {
            await Clients.Caller.OnlineUsersList(onlineUsers.Values);
            if (!onlineUsers.ContainsKey(user.Id))
            {
                onlineUsers.Add(user.Id, user);
                await Clients.Others.UserIsOnline(user.Id);
            }   
        }
    }
}
