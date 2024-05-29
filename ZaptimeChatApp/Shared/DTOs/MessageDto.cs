namespace ZaptimeChatApp.Shared.DTOs
{
    public record MessageDto(Guid ToUserId, Guid FromUserId, string Message, DateTime SentOn);
}
