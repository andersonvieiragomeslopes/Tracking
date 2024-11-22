using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tracking.BusinessLogicLayer.DTOs.Updates
{
    public abstract class BaseUpdate
    {
        public Guid Id { get; set; }
    }
}
