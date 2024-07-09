using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZaptimeChatApp.Shared.DTOs
{
    public class UserResetDto
    {
        public string Name { get; set; }
        public string UserName { get; set; }
        public string? PasswordRestToken { get; set; }
        public DateTime? ResetTokenExpires { get; set; }
    }
}
