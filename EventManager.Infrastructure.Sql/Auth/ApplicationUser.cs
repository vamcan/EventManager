using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace EventManager.Infrastructure.Auth
{
    // Add profile data for application users by adding properties to this class
    public class ApplicationUser : IdentityUser
    {
        // Extended Properties
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
