using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tracking.BusinessLogicLayer.DTOs
{
    public class TokenDTO
    {
        public required string Token { get; set; }

        public DateTime Expiration { get; set; }
    }
}
