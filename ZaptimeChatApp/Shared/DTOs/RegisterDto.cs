using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZaptimeChatApp.Shared.DTOs
{
    public class RegisterDto
    {
        [Required]
        public string Name { get; set; }
        [Required,  MaxLength(20)]
        public string UserName { get; set; }
        [Required, MaxLength(20), DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
