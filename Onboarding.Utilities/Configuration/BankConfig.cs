using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Onboarding.Utilities.Configuration
{
    public class BankConfig
    {
        public const string Config = "BankConfiguration";
        public string BaseUrl { get; set; }
        public string Resource { get; set; }
        public string Key { get; set; }
    }
}
