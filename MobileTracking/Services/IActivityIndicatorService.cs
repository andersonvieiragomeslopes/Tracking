using Controls.UserDialogs.Maui;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobileTracking.Services
{
    public interface IActivityIndicatorService
    {
        void StartIndicator();
        void EndIndicator();
        void UpdateIndicator(string message);
    }
    public class ActivityIndicatorService : IActivityIndicatorService
    {
        private readonly IUserDialogs userDialouge;
        public ActivityIndicatorService(IUserDialogs userDialog)
        {
            this.userDialouge = userDialog;
        }

        public void EndIndicator()
        {
            userDialouge.HideHud();
        }

        public void StartIndicator()
        {
            userDialouge.ShowLoading("Carregando..", MaskType.Black);

        }
        public void UpdateIndicator(string message)
        {
            userDialouge.CreateOrUpdateHud(new HudDialogConfig() { Message = message });

        }
    }
}
