namespace ZaptimeChatApp.Shared.DTOs
{
    public record MessageSendDto(Guid ToUserId, string Message);
}
