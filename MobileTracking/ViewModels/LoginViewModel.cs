using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using FluentValidation;
using FluentValidation.Results;
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
        public Guid? id = null;
        [ObservableProperty]
        private ValidationResult _validationResult = new ValidationResult();

        public LoginViewModel(INavigationService navigationService) : base(navigationService)
        {
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
                return;
            }

            var response = await _apiRequestService.AuthAsync(Id.Value);
                if(response.Token != null)
                await SecureStorage.SetAsync(Constants.AccessToken, response.Token);
            

        }
        [RelayCommand]
        public async Task Generate()
        {
          

            Id = await _apiRequestService.GenerateUserAsync();
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
                    var guidIdValid = Guid.TryParse(model.Id.ToString(), out var id);
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
