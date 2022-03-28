using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Onboarding.Core.Services.Interfaces;
using Onboarding.DTOs.Request;
using Onboarding.DTOs.Response;
using Onboarding.Models.Models;
using Onboarding.Models.Repositories;
using Onboarding.Utilities.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Onboarding.Core.Services.Implementations
{
    public class CustomerService : ICustomerService
    {

        private readonly UserManager<Customer> _userManager;
        private readonly IMapper _map;
        private readonly ICustomerRepository _Repo;
        private readonly IOTPService _Service;

        public CustomerService(UserManager<Customer> userManager, IMapper map, ICustomerRepository Repo, IOTPService Service)
        {
            _userManager = userManager;
            _map = map;
            _Repo = Repo;
            _Service = Service;
        }
        public async Task<BaseResponseDto<CustomerResponseDto>> AddCustomer(CustomerRequestDto model)
        {
            var cutomerToAdd = _map.Map<Customer>(model);
            cutomerToAdd.UserName = model.Email;
            var response = await _userManager.CreateAsync(cutomerToAdd);

            if (!response.Succeeded) return BaseResponseDto<CustomerResponseDto>.Fail($"{response.Errors.First()}", 400);

            var otp = _Service.GenerateOTP();
            //Send token to customer via email or sms
            //Emailsende.SendOtp(otp)

            var returnedUser = await _userManager.FindByEmailAsync(cutomerToAdd.Email);
            var mappedResponse = _map.Map<CustomerResponseDto>(returnedUser);

            return BaseResponseDto<CustomerResponseDto>.Success($"Customer created successfully", mappedResponse, 200);
        }

        public async Task<BaseResponseDto<string>> ConfirmOTP(string otp, string customerId)
        {
            var user = await _userManager.FindByIdAsync(customerId);
            if (user == null) return BaseResponseDto<string>.Fail($"User with id: {customerId} not found.", 404);

            if (user.PhoneNumberConfirmed) return BaseResponseDto<string>.Fail($"Customer already confirmed.", 400);

            //This would be assumed to be gotten via sms
            if (otp != _Service.OTP) return BaseResponseDto<string>.Fail("Confirming user phone number failed, Invalid OTP provided", 400);

            var token = await _userManager.GenerateChangePhoneNumberTokenAsync(user, user.PhoneNumber);
            if (string.IsNullOrWhiteSpace(token)) return BaseResponseDto<string>.Fail("Confirming user phone number failed", 400);

            var result = await _userManager
                .ChangePhoneNumberAsync(user, user.PhoneNumber, token);
            if (!result.Succeeded) return BaseResponseDto<string>.Fail("Confirming user phone number failed", 400);

            return BaseResponseDto<string>.Success("User confirmed successfully", null, 200);
        }

        public async Task<BaseResponseDto<PaginatorHelper<IEnumerable<CustomerResponseDto>>>> GetCustomersAsync(int pageSize = 10, int pageNumber = 1)
        {
            var response = _Repo.GetAllCustomers();
            if (response == null) return BaseResponseDto<PaginatorHelper<IEnumerable<CustomerResponseDto>>>.Fail("No customer found", 404);

            var pagedResponse = await Paginator.PaginateAsync<Customer, CustomerResponseDto>(response,
                                                                                             pageSize,
                                                                                             pageNumber,
                                                                                             _map);

            return BaseResponseDto<PaginatorHelper<IEnumerable<CustomerResponseDto>>>.Success("Customers retrieved successfully", pagedResponse, 200);
        }
    }
}
