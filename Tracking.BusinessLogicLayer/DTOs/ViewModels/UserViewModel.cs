using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tracking.BusinessLogicLayer.DTOs.ViewModels
{
    public class UserViewModel : ModelBaseViewModel
    {
        public UserViewModel(Guid id, DateTime createdAt, DateTime updatedAt) : base(id, createdAt, updatedAt)
        {
        }
    }
}
