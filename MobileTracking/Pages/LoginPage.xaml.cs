using MobileTracking.ViewModels;

namespace MobileTracking.Pages;

public partial class LoginPage : ContentPage
{
	public LoginPage()
	{
		InitializeComponent();
		BindingContext = ServiceLocator.Instance.Resolve<LoginViewModel>();
	}
}