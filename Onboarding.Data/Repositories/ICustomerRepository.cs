using Onboarding.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Onboarding.Models.Repositories
{
    public interface ICustomerRepository
    {
        IQueryable<Customer> GetAllCustomers();
    }
}
