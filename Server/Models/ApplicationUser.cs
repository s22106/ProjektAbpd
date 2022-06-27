using Microsoft.AspNetCore.Identity;
using Server.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjektAbpd.Server.Models
{
    public class ApplicationUser : IdentityUser
    {
        public virtual IEnumerable<DbUserStocks> Wishlisted { get; set; }
    }
}
