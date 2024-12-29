using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderMS.Application.DTOs.CompanyDTOs
{
    public class CompanyResDTO
    {
        public Guid CompanyId { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 3)]
        public string CompanyName { get; set; } = string.Empty;
        public string? CompanyDescription { get; set; }

        [StringLength(255)]
        public string? CompanyAddress { get; set; }

        [Required]
        [StringLength(100)]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        public DateTime CreatedAt { get; set; }

        public string? PicturePath { get; set; }
    }
}
