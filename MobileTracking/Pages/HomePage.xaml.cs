using MobileTracking.ViewModels;

namespace MobileTracking.Pages;

public partial class HomePage : ContentPage
{
    HomeViewModel vm;
    public HomePage(HomeViewModel homeViewModel)
	{
		InitializeComponent();
		BindingContext = vm = homeViewModel;
	}
    protected async override void OnAppearing()
    {
        if (AppShell.Index && BindingContext is HomeViewModel)
        {
            await vm.InitializeAsync(null);
            AppShell.Index = false;
        }
        base.OnAppearing();

    }
}