using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZaptimeChatApp.Shared.DTOs;

namespace ZaptimeChatApp.Shared
{
    public interface IZaptimeChatHubServer
    {
        Task SetUserOnline(UserDto user);
        Task SetUserOffline(UserDto user);
    }
}
