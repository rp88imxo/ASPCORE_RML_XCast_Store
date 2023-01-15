using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMLXCast.Core.Domain.Role
{
    public class ApplicationUserRole : IdentityRole
    {
        public string? Description { get; set; }
    }
}
