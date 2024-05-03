using System.ComponentModel.DataAnnotations;

namespace ZaptimeChatApp.Server.Data.Models
{
    public class Message
    {
        public Guid Id { get; set; }
        public Guid FromId { get; set; }
        public Guid ToId { get; set; }
        [Required, MaxLength(500)]
        public string Content { get; set; }
        public DateTime SentOn { get; set; }

        public virtual User FromUser { get; set; }
        public virtual User ToUser { get; set;}
    }
}
