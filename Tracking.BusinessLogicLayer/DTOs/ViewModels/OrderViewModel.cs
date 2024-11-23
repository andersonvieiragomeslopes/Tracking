using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tracking.DataAccessLayer.Entities;

namespace Tracking.BusinessLogicLayer.DTOs.ViewModels
{
    public class OrderViewModel : ModelBaseViewModel
    {
        public OrderViewModel(Guid id, Guid? userId, string title, string? description, string image, double latitude, double longitude, DateTime createdAt, DateTime updatedAt) : base(id, createdAt, updatedAt)
        {
            UserId = userId;
            Title = title;
            Image = image;
            Description = description;
            Latitude = latitude;
            Longitude = longitude;

        }
        public Guid? UserId { get; set; }
        public string Title { get; set; }
        public string Image { get; set; }
        public string? Description { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public UserViewModel? User { get; set; }

    }
}
