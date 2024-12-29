using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderMS.Application.DTOs.UserDTOs
{
    public class UpdateRoleReqDTO
    {
        [Required]
        public Guid UserId { get; set; }

        [Required]
        public string Role { get; set; } = string.Empty;
    }
}
