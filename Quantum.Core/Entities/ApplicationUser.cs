using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Quantum.Core.Entities
{
    public class ApplicationUser : IdentityUser
    {
        public string FullName { get; set; }
        public DateTime CreatedDate { get; private set; }

        public ApplicationUser()
        {
            CreatedDate = DateTime.UtcNow;
        }
    }
}
