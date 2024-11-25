using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Maui.GoogleMaps;
using Maui.GoogleMaps.Bindings;
using MobileTracking.Services;
using Shared.Extensions;
using Shared.Mobile.Services;
using Shared.Responses;
using Shared.SharedHubs;
using System.Collections.ObjectModel;
using System.Net.NetworkInformation;

namespace MobileTracking.ViewModels
{
    public partial class HomeViewModel : BaseViewModel
    {
        private readonly IInitalizeBackgroundService _initalizeBackgroundService;
        private readonly ILocationService _locationService;
        private readonly ISharedOrderHub _sharedOrderHub;
        public ObservableCollection<Pin> Pins { get; set; }
        public ObservableCollection<Polyline> Polylines { get; set; }
        [ObservableProperty] public MapSpan visibleRegion;
        [ObservableProperty] private MoveCameraRequest moveCameraRequest;
        [ObservableProperty] private AnimateCameraRequest animateCameraRequest;
        [ObservableProperty] private MoveToRegionRequest moveToRegionRequest;
        public HomeViewModel(INavigationService navigationService, IInitalizeBackgroundService initalizeBackgroundService, ILocationService locationService, ISharedOrderHub sharedOrderHub) : base(navigationService)
        {
            _initalizeBackgroundService = initalizeBackgroundService;
            _locationService = locationService;
            _sharedOrderHub = sharedOrderHub;
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

            _sharedOrderHub.OnOrderAdd += SharedOrderHub_OnOrderAdd;
            await _sharedOrderHub.StartAsync();
        }

        private async void SharedOrderHub_OnOrderAdd(object? sender, Guid orderId)
        {
            var response = await _apiRequestService.GetOrderAsync(orderId);
            if (response.IsSuccessStatusCode)
            {
                MainThread.BeginInvokeOnMainThread(() =>
                {
                    var pin = response?.Content;
                    Pins.Add(new Pin() { Label = pin.Title, Address = pin.Title, Position = new Position(pin.Latitude, pin.Longitude) });
                });
            }
        }

        [RelayCommand(AllowConcurrentExecutions = false)]
        public async Task Appearing()
        {
            try
            {
                var location = await _locationService.GetLocationAsync();
                var current = new Position(location.Latitude, location.Longitude);
                Pin pin = new Pin
                {
                    IsVisible = true,
                    Position = current,
                    Type = PinType.Place,
                    Label = "Me",
                    InfoWindowAnchor = new Point(100, 100)

                };
                Pins.Add(pin);

                VisibleRegion = MapSpan.FromCenterAndRadius(current, Distance.FromKilometers(5));
                AnimateCameraRequest = new AnimateCameraRequest();
                MoveCameraRequest = new MoveCameraRequest();
                MoveToRegionRequest = new MoveToRegionRequest();
                if (VisibleRegion != null)
                    await GoToCurrentLocation();
            }
            catch (Exception ex)
            {

            }
        }
        [RelayCommand(AllowConcurrentExecutions = false)]
        public async Task Disappearing()
        {
            _sharedOrderHub.OnOrderAdd -= SharedOrderHub_OnOrderAdd;
            await _sharedOrderHub.StopAsync();
        }
        public async Task GoToCurrentLocation()
        {
            await Task.Run(async () =>
            {
                await Task.Delay(1500);
                MainThread.BeginInvokeOnMainThread(() =>
                {
                    MoveToRegionRequest.MoveToRegion(VisibleRegion, true);
                });
            }).ConfigureAwait(false);
        }
        [RelayCommand(AllowConcurrentExecutions = false)]
        public async Task PinClickedTo(PinClickedEventArgs e)
        {
            e.Handled = true;
            if (!e.Pin.Label.Equals("Me"))
                await GetRoute(e.Pin);
        }
        [RelayCommand]
        public async Task GetRoute(Pin pin)
        {
            IsBusy = true;
            var position = new PositionResponse();
            position.ToLatitude = pin.Position.Latitude;
            position.Tolongitude = pin.Position.Longitude;
            LoadingText = "Buscando sua localização atual..";
            var currentLocation = await _locationService.GetLocationAsync();
            if (currentLocation != null)
            {
                position.FromLatitude = currentLocation.Latitude;
                position.Fromlongitude = currentLocation.Longitude;
                LoadingText = "Buscando seu destino..";
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
                            Polylines.Add(track);

                            LoadingText = "Destino encontrado..";
                            await Task.Delay(3000);
                            await AnimateRoute();
                        }
                    }
                }
            }
            IsBusy = false;
        }
        private async Task AnimateRoute()
        {
            try
            {
                var startPosition = Polylines.FirstOrDefault().Positions.FirstOrDefault();
                var endPosition = Polylines.FirstOrDefault().Positions.LastOrDefault();

                var cameraUpdate = CameraUpdateFactory.NewPositionZoom(startPosition, 15);
                LoadingText = "Validando a rota..";
                await AnimateCameraRequest.AnimateCamera(cameraUpdate, TimeSpan.FromSeconds(3));

                var bounds = GetBoundsForPositions(Polylines.FirstOrDefault().Positions.ToList());

                cameraUpdate = CameraUpdateFactory.NewBounds(bounds, 150);
                LoadingText = "Verificando distancia..";
                await AnimateCameraRequest.AnimateCamera(cameraUpdate, TimeSpan.FromSeconds(3));

                await AnimateCameraRequest.AnimateCamera(CameraUpdateFactory.NewPositionZoom(endPosition, 15), TimeSpan.FromSeconds(2));
                LoadingText = "Verificando trajeto..";
                cameraUpdate = CameraUpdateFactory.NewBounds(bounds, 150);

                await AnimateCameraRequest.AnimateCamera(cameraUpdate, TimeSpan.FromSeconds(3));
                LoadingText = "Tudo certo, boa viagem.";
                await AnimateCameraRequest.AnimateCamera(CameraUpdateFactory.NewPositionZoom(startPosition, 40), TimeSpan.FromSeconds(2));

                double angle = GetAngle(startPosition, endPosition);
                var cameraPosition = new CameraPosition(
                    startPosition,
                    60,
                    angle,
                    0
                    );
                await AnimateCameraRequest.AnimateCamera(CameraUpdateFactory.NewCameraPosition(cameraPosition), TimeSpan.FromSeconds(2));
            }
            catch (Exception ex)//prevenir possíveis crashs na animação
            {
                IsBusy = false;
            }

        }
        private double GetAngle(Position start, Position end)
        {
            double deltaLongitude = end.Longitude - start.Longitude;
            double deltaLatitude = end.Latitude - start.Latitude;
            double angle = Math.Atan2(deltaLongitude, deltaLatitude) * (180 / Math.PI);

            return angle;
        }
        private Bounds GetBoundsForPositions(List<Position> positions)
        {
            double minLat = positions.Min(p => p.Latitude);
            double maxLat = positions.Max(p => p.Latitude);
            double minLon = positions.Min(p => p.Longitude);
            double maxLon = positions.Max(p => p.Longitude);

            return new Bounds(new Position(minLat, minLon), new Position(maxLat, maxLon));
        }

    }
}
