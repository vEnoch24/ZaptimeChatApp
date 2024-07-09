using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZaptimeChatApp.Shared.RequestPayload
{
    public class ResetPasswordRequest
    {
        [Required]
        public string Token { get; set; }

        [Required, MaxLength(20), DataType(DataType.Password)]
        public string Password { get; set; } = string.Empty;

        [Required, Compare("Password")]
        public string confirmPassword { get; set; } = string.Empty;
    }
}
