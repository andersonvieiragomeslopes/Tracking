using CommunityToolkit.Mvvm.ComponentModel;
using MobileTracking.Services;
using Shared.Mobile;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobileTracking.ViewModels
{
    public partial class LoadingViewModel : BaseViewModel
    {
        [ObservableProperty]
        public string loadingMessage = "";

        public LoadingViewModel(INavigationService navigationService) : base(navigationService)
        {
        }

        public async override Task InitializeAsync(object navigationData)
        {
            //Task.Run(async () => {             
            LoadingMessage = "Carregando dados do mapa...";
            await Task.Delay(3000);
            LoadingMessage = "Carregando dados do usuário...";
            await ApplicationDbConnection.InitializeAsync();
            LoadingMessage = "Finalizando...";
            await Task.Delay(1000);
            var token = await SecureStorage.GetAsync(Constants.AccessToken);
            if (!string.IsNullOrEmpty(token))
                _navigationService.NavigateMainPage(MainPages.AppShell);
            else
                _navigationService.NavigateMainPage(MainPages.LoginPage);
            //}).ConfigureAwait(false);//não travar o thread principal de forma desnecessária.
        }
    }
}
