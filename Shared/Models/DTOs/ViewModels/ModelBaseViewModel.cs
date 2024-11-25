using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Shared.Models.DTOs.ViewModels
{
    public class ModelBaseViewModel
    {
        public ModelBaseViewModel(Guid id, DateTime createdAt, DateTime updatedAt)
        {
            Id = id;
            CreatedAt = DateOnly.FromDateTime(createdAt);
            UpdatedAt = DateOnly.FromDateTime(updatedAt);
        }    

        public Guid Id { get; set; }
        public DateOnly CreatedAt { get; set; }
        public DateOnly UpdatedAt { get; set; }
    }
}
