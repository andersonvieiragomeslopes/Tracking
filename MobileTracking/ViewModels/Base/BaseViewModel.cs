using CommunityToolkit.Mvvm.ComponentModel;
//using IntelliJ.Lang.Annotations;
using MobileTracking.Services;
using Shared.Mobile.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobileTracking
{
    public abstract partial class BaseViewModel : ObservableObject, IQueryAttributable
    {
        protected readonly INavigationService _navigationService;
        protected readonly IApiRequestService _apiRequestService = ServiceLocator.Instance.Resolve<IApiRequestService>();
        private readonly IActivityIndicatorService _activityIndicatorService = ServiceLocator.Instance.Resolve<IActivityIndicatorService>();
        [ObservableProperty]
        public string _loadingText;
        [ObservableProperty]
        private bool isBusy = false;
        protected BaseViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
        }
        public virtual Task InitializeAsync(object navigationData)
        {
            InitializeAsync();
            return Task.FromResult(false);
        }
        protected virtual Task InitializeAsync()
        {
            return Task.FromResult(true);
        }
        protected void SetDataLoadingIndicators(bool isStarting = true)
        {
            if (isStarting)
            {
                IsBusy = true;
            }
            else
            {
                LoadingText = "";
                IsBusy = false;
            }
        }
        partial void OnLoadingTextChanged(string value)
        {
            if (!string.IsNullOrEmpty(value))
                _activityIndicatorService.UpdateIndicator(value);
        }
        partial void OnIsBusyChanged(bool value)
        {
            if (value)
            {
                _activityIndicatorService.StartIndicator();
            }

            else
            {
                _activityIndicatorService.EndIndicator();
            }
        }
        public async void ApplyQueryAttributes(IDictionary<string, object> query)
        {
            await InitializeAsync(query.Values.FirstOrDefault()).ConfigureAwait(false);
        }
    }
}
