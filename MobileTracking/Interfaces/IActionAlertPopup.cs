using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobileTracking.Interfaces
{
    public interface IActionAlertPopup
    {
        string Message { get; set; }

        string MessageTitle { get; set; }

        string CancelButton { get; set; }
    }
}
