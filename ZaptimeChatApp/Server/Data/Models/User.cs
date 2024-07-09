using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ZaptimeChatApp.Server.Data.Models
{
    [Table("User")]
    public class User
    {
        [Key]
        public Guid Id { get; set; }
        [Required, Unicode(false)]
        public string Name { get; set; }
        public DateTime DateAdded { get; set; } = DateTime.Now;
        [Required, Unicode(false), MaxLength(20)]
        public string UserName { get; set; }
        public byte[] passwordhash { get; set; } = new byte[32];
        public byte[] passwordSalt { get; set; } = new byte[32];
        public string? PasswordRestToken { get; set; }
        public DateTime? ResetTokenExpires { get; set; }
    }
}
