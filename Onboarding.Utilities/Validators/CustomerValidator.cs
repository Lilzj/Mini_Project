using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Onboarding.Data;
using Onboarding.DTOs.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Onboarding.Utilities.Validators
{
    public class CustomerValidator : AbstractValidator<CustomerRequestDto>
    {
        public CustomerValidator(OnboardingDbContext _ctx)
        {
            RuleFor(q => q.State).NotEmpty().NotNull().WithMessage("State is required");
            RuleFor(q => q.PhoneNumber).NotEmpty().NotNull().WithMessage("Phone number is required")
                .MaximumLength(11).WithMessage("Phone number cannot be more than 11 digits")
                .MinimumLength(11).WithMessage("Phone number cannot be less than 11 digits")
                .Matches(@"^[+-]?([0-9]+\.?[0-9]*|\.[0-9]+)$").WithMessage("Phone number can only contain numerical values");
            RuleFor(q => q.Password).NotEmpty().NotNull().WithMessage("Password is required");
            RuleFor(q => q.LGA).NotEmpty().NotNull().WithMessage("LGA is required");

            When(q => !String.IsNullOrEmpty(q.LGA), () =>
            {
                RuleFor(p => p).Custom((value, context) =>
                {
                    var state = _ctx.States.Include(l => l.LGAs).Where(x => x.Name == value.State).FirstOrDefaultAsync().Result;
                    if (!state.LGAs.Any(l => l.Name == value.LGA))
                    {
                        context.AddFailure($"{value.LGA} is not a valid local government area for {value.State}.");
                    }
                });
            });

        }
    }
}
