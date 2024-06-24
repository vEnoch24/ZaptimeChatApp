using Microsoft.Extensions.Options;
using Vonage;
using Vonage.Request;
using ZaptimeChatApp.Shared.DTOs;

namespace ZaptimeChatApp.Server.Services
{
    public class VonageServices
    {
        private readonly VonageClient _client;

        public VonageServices(IOptions<VonageSetting> vonageSettings)
        {
            var settings = vonageSettings.Value;

            var creds = Credentials.FromApiKeyAndSecret(settings.ApiKey, settings.ApiSecret);
            _client = new VonageClient(creds);
        }

    }
}
