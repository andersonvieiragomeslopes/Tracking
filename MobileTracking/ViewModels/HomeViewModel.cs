using MobileTracking.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobileTracking.ViewModels
{
    public partial class HomeViewModel : BaseViewModel
    {
        private readonly IInitalizeBackgroundService _initalizeBackgroundService;
        public HomeViewModel(INavigationService navigationService, IInitalizeBackgroundService initalizeBackgroundService) : base(navigationService)
        {
            _initalizeBackgroundService = initalizeBackgroundService;
        }
        public override Task InitializeAsync(object navigationData)
        {
            _initalizeBackgroundService.StartSyncOrders();
            return base.InitializeAsync(navigationData);
        }
    }
}
