using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Onboarding.Core.Services.Interfaces
{
    public interface IOTPService
    {
        public string OTP { get; }
        string GenerateOTP();
    }
}
