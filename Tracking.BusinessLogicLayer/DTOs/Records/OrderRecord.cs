using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tracking.BusinessLogicLayer.DTOs.Records
{
    public class OrderRecord
    {
        public string Title { get; set; } = null!;
        public string Image { get; set; } = null!;
        public string? Description { get; set; }
        public double Longitude { get; set; }
        public double Latitude { get; set; }
        public Guid UserId { get; set; }
    }
}
