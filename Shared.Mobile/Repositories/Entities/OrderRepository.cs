using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Mobile.Repositories
{
    public class OrderRepository : BaseRepository
    {
        public Guid UserId { get; set; }

        public string? Title { get; set; }

        public string? Address { get; set; }

        public string? Description { get; set; }

        public double Latitude { get; set; }

        public double Longitude { get; set; }
    }
}
