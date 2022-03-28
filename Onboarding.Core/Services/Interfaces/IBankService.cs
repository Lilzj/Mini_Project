using Onboarding.DTOs.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Onboarding.Core.Services.Interfaces
{
    public interface IBankService
    {
        Task<BankResponseDto> GetBanksAsync();
    }
}
