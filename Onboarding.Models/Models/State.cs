using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Onboarding.Models.Models
{
    public class State
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<LGA> LGAs { get; set; }

        public State()
        {
            LGAs = new List<LGA>();
        }
    }
}
