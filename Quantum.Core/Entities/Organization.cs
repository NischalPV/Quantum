using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Quantum.Core.Entities
{
    public class Organization : BaseEntity<string>
    {
        public string Name { get; set; }
        public virtual List<OrganizationUser> OrganizationUsers { get; set; }
    }

    public class OrganizationUser : BaseEntity<int>
    {
        [ForeignKey(nameof(Organization))]
        public string OrganizationId { get; set; }
        
        [ForeignKey(nameof(User))]
        public string UserId { get; set; }

        public virtual Organization Organization { get; set; }
        public virtual ApplicationUser User { get; set; }
    }
}
