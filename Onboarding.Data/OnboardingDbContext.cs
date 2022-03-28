using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Onboarding.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Onboarding.Data
{
    public class OnboardingDbContext : IdentityDbContext<Customer>
    {
        public OnboardingDbContext(DbContextOptions<OnboardingDbContext> options) : base(options) { }

        public DbSet<Customer> Customers { get; set; }
        public DbSet<State> States { get; set; }
        public DbSet<LGA> LGAs { get; set; }
    }
}
