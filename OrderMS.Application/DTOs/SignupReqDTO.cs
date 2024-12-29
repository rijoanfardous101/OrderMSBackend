using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderMS.Application.DTOs
{
    public class SignupReqDTO
    {
        [Required]
        [StringLength(100, MinimumLength = 4)]
        public string CompanyName { get; set; } = string.Empty;

        [Required]
        //[DataType(DataType.EmailAddress)]
        [EmailAddress]
        public string EmailAddress {  get; set; } = string.Empty;

        [Required]
        [DataType (DataType.Password)]
        [MinLength(6)]
        public string Password { get; set; } = string.Empty;
    }
}
