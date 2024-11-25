using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Models.DTOs.ViewModels
{
    public class UserViewModel : ModelBaseViewModel
    {
        public UserViewModel(Guid id, string name, DateTime createdAt, DateTime updatedAt) : base(id, createdAt, updatedAt)
        {
            Name = name;
        }
        public string Name { get; set; }
    }
}
