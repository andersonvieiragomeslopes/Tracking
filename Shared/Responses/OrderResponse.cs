using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Responses
{
    public class OrderResponse
    {
        public string Title { get; set; }
        public string Image { get; set; }
        public string? Description { get; set; }
        public double Longitude { get; set; }
        public double Latitude { get; set; }
        public Guid? UserId { get; set; }
    }
}
