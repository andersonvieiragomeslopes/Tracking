using MobileTracking.Pages;
using Mopups.Interfaces;
using Mopups.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobileTracking.Services
{
    public enum MainPages
    {
        LoginPage,
        LoadingPage,
        AppShell
    }
    public interface INavigationService
    {
        Task NavigateToAsync(string route, object obj = null);
        Task NavigateBackAsync();
        Task NavigateBackAsync(object obj);
        Task PushPopupAsync<T>(Action<T> action = null);
        Task PopPopupAsync();
        void NavigateMainPage(MainPages mainPages, bool isFirstLoad = false);
    }
    public class NavigationService : INavigationService
    {
        private IPopupNavigation _popupNavigation;
        private Application CurrentApplication => Application.Current;
        public NavigationService(IPopupNavigation popupNavigation)
        {
            _popupNavigation = popupNavigation;
        }
        public Task NavigateToAsync(string route, object obj)
        {
            var shellNavigation = new ShellNavigationState(route);

            return Shell.Current.GoToAsync(shellNavigation, false, new Dictionary<string, object> { { "key", obj } });
        }

        public void NavigateMainPage(MainPages mainPages, bool isFirstLoad)
        {
            switch (mainPages)
            {
                case MainPages.LoginPage:
                    //CurrentApplication.MainPage = new NavigationPage(new LoginPage());
                    break;
                case MainPages.AppShell:
                    CurrentApplication.MainPage = new AppShell();
                    break;
                //case MainPages.LoadingPage:
                //    CurrentApplication.MainPage = new NavigationPage(new LoadingPage(IoCContainer.Instance.Resolve<LoadingViewModel>()));
                 //   break;
                default:
                    break;
            }
        }

        public Task NavigateBackAsync() =>
            Shell.Current.GoToAsync("..");
        public Task NavigateBackAsync(object obj) =>
            Shell.Current.GoToAsync("..", new Dictionary<string, object> { { "key", obj } });
        public Task PushPopupAsync<T>(Action<T> action)
        {
            var popup = ServiceLocator.Instance.Resolve<T>();

            if (popup is PopupPage popupPage)
            {
                return TryInvokeOnMainThread(() =>
                {
                    action?.Invoke(popup);

                    async Task showPopupBeforeLoader()
                    {
                        if (_popupNavigation.PopupStack.Any())
                        {
                            await _popupNavigation.PopAsync();
                        }

                        await _popupNavigation.PushAsync(popupPage);
                    }

                    return showPopupBeforeLoader();
                });
            }
            return Task.CompletedTask;
        }
        private Task TryInvokeOnMainThread(Func<Task> value)
        {
            if (MainThread.IsMainThread)
            {
                return value.Invoke();
            }
            else
            {
                return MainThread.InvokeOnMainThreadAsync(value);
            }
        }
        public async Task PopPopupAsync()
        {

            await _popupNavigation.PopAsync(true);
        }
    }
}
