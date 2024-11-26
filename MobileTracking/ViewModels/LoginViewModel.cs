using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using FluentValidation;
using FluentValidation.Results;
using MobileTracking.Interfaces;
using MobileTracking.Services;
using Shared.Mobile;
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

        [ObservableProperty]
        public string? id = null;
        //DEBUG
        [ObservableProperty]
        private ValidationResult _validationResult = new ValidationResult();

        public LoginViewModel(INavigationService navigationService) : base(navigationService)
        {
#if DEBUG
            Id = "38b5e825-63f2-456b-8095-a960f709575e";
#endif
        }
        public override Task InitializeAsync(object navigationData)
        {
            return base.InitializeAsync(navigationData);
        }
        protected override Task InitializeAsync()
        {
            return base.InitializeAsync();
        }
        [RelayCommand]
        public async Task Login()
        {
            var validator = new LoginPageViewModelValidator();

            this.ValidationResult = await validator.ValidateAsync(this);
            if (!ValidationResult.IsValid)
            {
                await _navigationService.PushPopupAsync<IActionAlertPopup>(p =>
                {
                    p.MessageTitle = "Erro!";
                    p.Message = "Id de usuário invalido.";
                    p.CancelButton = "Ok";
                }).ConfigureAwait(false);
                return;
            }

            var response = await _apiRequestService.AuthAsync(Guid.Parse(Id));
            if (response.Token != null)
            {
                await SecureStorage.SetAsync(Constants.AccessToken, response.Token);
                await SecureStorage.SetAsync(Constants.Id, Id.ToString()!);
                _navigationService.NavigateMainPage(MainPages.AppShell);
            }

        }
        [RelayCommand]
        public async Task Generate()
        {
            var response = await _apiRequestService.GenerateUserAsync();
            if (response.IsSuccessStatusCode)
            {
                Id = response?.Content.ToString();
            }
            else
                await _navigationService.PushPopupAsync<IActionAlertPopup>(p =>
                {
                    p.MessageTitle = "Erro!";
                    p.Message = "Não foi possível gerar o seu Id de usuário.";
                    p.CancelButton = "Ok";
                }).ConfigureAwait(false);
        }
        //https://medium.com/@chausse.nicolas/input-validation-in-net-maui-with-fluentvalidation-and-syncfusion-toolkit-ab8e7fc05e2d
        public class LoginPageViewModelValidator : AbstractValidator<LoginViewModel>
        {
            public static string IdProperty => nameof(Id);
            public static string GlobalProperty => "Global";

            public LoginPageViewModelValidator()
            {

                RuleFor(x => x).Custom((model, context) =>
                {
                    var guidIdValid = Guid.TryParse(model?.Id, out var id);
                    if (guidIdValid)
                    {
                        return;
                    }

                    context.AddFailure(GlobalProperty, "UserId invalid or not found.");
                });
            }
        }
    }

}
