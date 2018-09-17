using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientDto.Api
{
    public class AddressPostFeeDto
    {
        public string Province { get; set; }
        public int UnitPostFee { get; set; }
        public string UnitPostFeeStr
        {
            get { return ((float)(UnitPostFee / 100.00)).ToString("F2"); }
        }
    }
}