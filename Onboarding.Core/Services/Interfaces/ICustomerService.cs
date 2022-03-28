using Onboarding.DTOs.Request;
using Onboarding.DTOs.Response;
using Onboarding.Utilities.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Onboarding.Core.Services.Interfaces
{
    public interface ICustomerService
    {
        Task<BaseResponseDto<CustomerResponseDto>> AddCustomer(CustomerRequestDto model);
        Task<BaseResponseDto<PaginatorHelper<IEnumerable<CustomerResponseDto>>>> GetCustomersAsync(int pageSize = 10, int pageNumber = 1);
        Task<BaseResponseDto<string>> ConfirmOTP(string otp, string customerId);
    }
}
