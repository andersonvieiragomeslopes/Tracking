using CommunityToolkit.Mvvm.ComponentModel;
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

        public async override Task InitializeAsync(object navigationData)
        {
         
            LoadingMessage = "Carregando dados do mapa...";
            await Task.Delay(3000);
            LoadingMessage = "Carregando dados do usuário...";
            await Task.Delay(3000);
            LoadingMessage = "Finalizando...";

            var token = await SecureStorage.GetAsync(Constants.AccessToken);
            if (!string.IsNullOrEmpty(token))
            {
            }
        }
    }
}
