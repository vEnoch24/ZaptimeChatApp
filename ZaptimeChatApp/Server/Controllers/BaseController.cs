using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace ZaptimeChatApp.Server.Controllers
{
    [Authorize]
    public abstract class BaseController : ControllerBase
    {
        private string userId;

        public Guid UserId 
        {
            get {

                if(userId == null)
                {
                    userId = Convert.ToString(User.Claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value);
                }
                return new Guid(userId);
            } 
        }
    }
}
