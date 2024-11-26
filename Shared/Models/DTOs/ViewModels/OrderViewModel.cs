using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Models.DTOs.ViewModels
{
    public class OrderViewModel : ModelBaseViewModel
    {
        public OrderViewModel(Guid id, Guid? userId, string title, string? description, string address, double latitude, double longitude, DateTime createdAt, DateTime updatedAt) : base(id, createdAt, updatedAt)
        {
            UserId = userId;
            Title = title;
            Address = address;
            Description = description;
            Latitude = latitude;
            Longitude = longitude;

        }
        public Guid? UserId { get; set; }
        public string Title { get; set; }
        public string Address { get; set; }
        public string? Description { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public UserViewModel? User { get; set; }

    }
}
