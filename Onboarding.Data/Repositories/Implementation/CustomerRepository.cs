using Onboarding.Data;
using Onboarding.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Onboarding.Models.Repositories.Implementation
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly OnboardingDbContext _db;

        public CustomerRepository(OnboardingDbContext db)
        {
            _db = db;
        }

        public IQueryable<Customer> GetAllCustomers()
        {
            return _db.Customers.AsQueryable();
        }
    }
}
