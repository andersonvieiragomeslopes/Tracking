﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Models.DTOs.Records
{
    public class OrderRecord
    {
        public string Title { get; set; } = null!;
        public string Address { get; set; } = null!;
        public string? Description { get; set; }
        public double Longitude { get; set; }
        public double Latitude { get; set; }
        public Guid UserId { get; set; }
    }
}
