using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ZaptimeChatApp.Server.Data.Models
{
    public class Message
    {
        [Key]
        public Guid Id { get; set; }
        public Guid FromId { get; set; }
        public Guid ToId { get; set; }
        [Required, MaxLength(500)]
        public string Content { get; set; }
        public DateTime SentOn { get; set; }

        [ForeignKey(nameof(Message.FromId))]
        public virtual User FromUser { get; set; }
        [ForeignKey(nameof(Message.ToId))]
        public virtual User ToUser { get; set;}
    }
}
