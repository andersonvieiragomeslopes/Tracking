using Maui.GoogleMaps;
using MobileTracking.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobileTracking.ViewModels
{
    public partial class HomeViewModel : BaseViewModel
    {
        private readonly IInitalizeBackgroundService _initalizeBackgroundService;

        public ObservableCollection<Pin> Pins { get; set; }

        public HomeViewModel(INavigationService navigationService, IInitalizeBackgroundService initalizeBackgroundService) : base(navigationService)
        {
            _initalizeBackgroundService = initalizeBackgroundService;
        }
        public async override Task InitializeAsync(object navigationData)
        {
            _initalizeBackgroundService.StartSyncOrders();
            var response = await _apiRequestService.MyOrdersAsync();
            if(response.IsSuccessStatusCode)
            foreach (var pin in response.Content) 
            {
                Pins.Add(new Pin() { Label = pin.Title, Address = pin.Title, Position = new Position(pin.Latitude, pin.Longitude)});
            }
        }
    }
}
