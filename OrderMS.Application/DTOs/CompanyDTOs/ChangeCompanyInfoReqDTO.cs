using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderMS.Application.DTOs.CompanyDTOs
{
    public class ChangeCompanyInfoReqDTO
    {
        public string? Description { get; set; }
        public string? Address { get; set; }
    }
}
