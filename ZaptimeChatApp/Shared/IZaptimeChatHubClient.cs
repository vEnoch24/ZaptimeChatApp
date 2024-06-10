using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZaptimeChatApp.Shared.DTOs;

namespace ZaptimeChatApp.Shared
{
    public interface IZaptimeChatHubClient
    {
        Task UserConnected(UserDto user);
        Task OnlineUsersList(IEnumerable<UserDto> users);
        Task UserIsOnline(Guid userId);
        Task UserIsOffline(Guid userId);
        Task MessageReceived(MessageDto messageDto);
    }
}
