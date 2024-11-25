using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Models.DTOs.Updates
{
    public abstract class BaseUpdate
    {
        public Guid Id { get; set; }
    }
}
