using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BullPerks_TestWork.Domain.ViewModels
{
    public class TokenViewModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public float TotalSupply { get; set; }
        public float CirculatingSupply { get; set; }
    }
}
