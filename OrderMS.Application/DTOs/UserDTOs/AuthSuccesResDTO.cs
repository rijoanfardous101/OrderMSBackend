using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderMS.Application.DTOs.UserDTOs
{
    public class AuthSuccesResDTO
    {
        public string? JwtToken { get; set; } = string.Empty;
        public string? EmailAddress { get; set; } = string.Empty;
        public string? Role { get; set; } = string.Empty;
    }
}
