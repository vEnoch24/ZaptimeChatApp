using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using OpenTokSDK;
using Vonage;
using Vonage.Request;
using Vonage.Video;
using ZaptimeChatApp.Shared.DTOs;

namespace ZaptimeChatApp.Server.Controllers
{
    public class VideoController : BaseController
    {
        private readonly OpenTok _openTok;
        //private readonly VonageClient _vonageClient;

        public VideoController(IOptions<VonageSetting> vonageSettings)
        {
            var settings = vonageSettings.Value;

            if (!int.TryParse(settings.ApiKey, out int apiKey))
            {
                throw new InvalidOperationException("API Key must be a numeric string.");
            }

            _openTok = new OpenTok(apiKey, settings.ApiSecret);
        }

        [HttpGet("session")]
        public IActionResult CreateSession()
        {
            var session = _openTok.CreateSession();
            return Ok(new { SessionId = session.Id });
        }

        [HttpGet("token")]
        public IActionResult GenerateToken(string sessionId)
        {
            var token = _openTok.GenerateToken(sessionId);
            return Ok(new { Token = token });
        }
    }
}
