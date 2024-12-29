using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace OrderMS.Domain.Entities
{
    public class ApplicationUser : IdentityUser
    {
        public Guid CompanyId { get; set; }

        public virtual Company Company { get; set; }
    }
}
