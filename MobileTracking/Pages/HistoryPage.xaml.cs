using Shared.Mobile;

namespace MobileTracking.Pages;

public partial class HistoryPage : ContentPage
{
	public HistoryPage()
	{
		InitializeComponent();
	}
    protected async override void OnAppearing()
    {
        UserId.Text = await SecureStorage.GetAsync(Constants.Id);
        base.OnAppearing();
    }
}