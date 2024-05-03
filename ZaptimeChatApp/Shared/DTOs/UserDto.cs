namespace ZaptimeChatApp.Shared.DTOs
{
    public class UserDto
    {
        public UserDto(Guid Id, string Name, bool isOnline = false)
        {
            this.Id = Id;
            this.Name = Name;
            this.IsOnline = isOnline;
        }

        public Guid Id { get; set; }
        public string Name { get; set; }
        public bool IsOnline { get; set; }
        public bool IsSelected { get; set; }
    }
}
