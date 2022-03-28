using FluentValidation;
using Onboarding.DTOs.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Onboarding.Utilities.Validators
{
    public class PhoneNumberValidator : AbstractValidator<OTPRequestDto>
    {
        public PhoneNumberValidator()
        {
            RuleFor(q => q.OTP).NotEmpty().NotNull().WithMessage("OTP is required")
                .MaximumLength(6).WithMessage("OPT cannot be more than 6 characters.")
                .MinimumLength(6).WithMessage("OPT cannot be less than 6 characters.")
                .Matches(@"^[+-]?([0-9]+\.?[0-9]*|\.[0-9]+)$").WithMessage("OPT can only contain numerical values");
        }
    }
}
