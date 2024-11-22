using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Shared.Mobile.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobileTracking.ViewModels
{
    public partial class LoginViewModel : BaseViewModel
    {
        private readonly IApiRequestService _apiRequestService;

        [ObservableProperty]
        public Guid? _id = null;


        public LoginViewModel(IApiRequestService apiRequestService)
        {
            _apiRequestService = apiRequestService;
        }
        [RelayCommand]
        public async Task Login()
        {
            if (Id.HasValue)
            {
                var response = await _apiRequestService.AuthAsync(Id.Value);
            }

        }
        [RelayCommand]
        public async Task Generate()
        {
            Id = await _apiRequestService.GenerateUserAsync();
        }
    }
}
