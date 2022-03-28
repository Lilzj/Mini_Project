using AutoMapper;
using Onboarding.DTOs.Request;
using Onboarding.DTOs.Response;
using Onboarding.Models.Models;

namespace Onboarding.Mapper
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<Customer, CustomerResponseDto>()
                .ForMember(CustomerResponseDto => CustomerResponseDto.IsCustomerConfirmed, Customer => Customer.MapFrom(p => p.PhoneNumberConfirmed))
                .ForMember(CustomerResponseDto => CustomerResponseDto.State, Customer => Customer.MapFrom(p => p.State))
                .ForMember(CustomerResponseDto => CustomerResponseDto.LGA, Customer => Customer.MapFrom(p => p.LGA));

            CreateMap<CustomerRequestDto, Customer>();
        }
    }
}
