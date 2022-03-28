using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Onboarding.Models.Models
{
    public class Customer : IdentityUser
    {
        public string State { get; set; }
        public string LGA { get; set; }
    }
}
