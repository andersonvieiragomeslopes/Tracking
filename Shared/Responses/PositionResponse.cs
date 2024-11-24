using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Responses
{
    public class PositionResponse
    {
        public double ToLatitude { get; set; }
        public double FromLatitude { get; set; }
        public double Tolongitude { get; set; }
        public double Fromlongitude { get; set; }
    }
}
