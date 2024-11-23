using MobileTracking.ViewModels;

namespace MobileTracking.Pages;

public partial class LoadingPage : ContentPage
{
    LoadingViewModel vm;
    public LoadingPage(LoadingViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = vm = viewModel;
	}
    protected async override void OnAppearing()
    {
        base.OnAppearing();
        await vm.InitializeAsync(null).ConfigureAwait(false);
    }
    protected override bool OnBackButtonPressed()
    {
        return true;
    }
}