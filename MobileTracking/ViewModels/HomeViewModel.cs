using CommunityToolkit.Mvvm.Input;
using Maui.GoogleMaps;
using MobileTracking.Services;
using Shared.Extensions;
using Shared.Mobile.Services;
using Shared.Responses;
using System.Collections.ObjectModel;

namespace MobileTracking.ViewModels
{
    public partial class HomeViewModel : BaseViewModel
    {
        private readonly IInitalizeBackgroundService _initalizeBackgroundService;
        private readonly ILocationService _locationService;
        public ObservableCollection<Pin> Pins { get; set; }

        public HomeViewModel(INavigationService navigationService, IInitalizeBackgroundService initalizeBackgroundService, ILocationService locationService) : base(navigationService)
        {
            _initalizeBackgroundService = initalizeBackgroundService;
            _locationService = locationService;
        }
        public async override Task InitializeAsync(object navigationData)
        {
            _initalizeBackgroundService.StartSyncOrders();
            var response = await _apiRequestService.MyOrdersAsync();
            if (response.IsSuccessStatusCode)
                foreach (var pin in response.Content)
                {
                    Pins.Add(new Pin() { Label = pin.Title, Address = pin.Title, Position = new Position(pin.Latitude, pin.Longitude) });
                }
        }

        [RelayCommand(AllowConcurrentExecutions = false)]
        public async Task PinClickedTo(PinClickedEventArgs e)
        {
            e.Handled = true;
            await GetRoute(e.Pin);
        }
        [RelayCommand]
        public async Task GetRoute(Pin pin)
        {
            var position = new PositionResponse();
            position.ToLatitude = pin.Position.Latitude;
            position.Tolongitude = pin.Position.Longitude;
            var currentLocation = await _locationService.GetLocationAsync();
            if (currentLocation != null)
            {
                position.FromLatitude = currentLocation.Latitude;
                position.Fromlongitude = currentLocation.Longitude;
                var response = await _apiRequestService.DrivingAsync(position);

                if (response.IsSuccessStatusCode)
                {
                    var polylineCoded = response?.Content?.Routes?.FirstOrDefault();
                    if (polylineCoded != null)
                    {
                        var polylineDecoded = polylineCoded.Geometry.Decode();
                        if (polylineDecoded != null && polylineDecoded.Any())
                        {
                            var track = new Polyline
                            {
                                StrokeWidth = 5,
                                StrokeColor = Color.FromArgb("#39C5BB"),
                            };
                            polylineDecoded.ForEach(p => { track.Positions.Add(new Position(p.Latitude, p.Longitude)); });

                        }
                    }
                }
            }
        }

    }
}
