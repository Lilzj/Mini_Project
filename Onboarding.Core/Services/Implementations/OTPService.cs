using Onboarding.Core.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Onboarding.Core.Services.Implementations
{
    public class OTPService : IOTPService
    {
        private string Otp = "546723";
        public string OTP { get => Otp; }

        public string GenerateOTP()
        {
            //This implimentation would generate random 6 digits otp value. :)
            return OTP;
        }
    }
}
