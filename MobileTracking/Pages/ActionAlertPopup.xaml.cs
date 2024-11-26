using MobileTracking.Interfaces;
using Mopups.Pages;
using Mopups.Services;

namespace MobileTracking.Pages;

public partial class ActionAlertPopup : PopupPage, IActionAlertPopup
{
    public string Message
    {
        get => description.Text;
        set => MainThread.BeginInvokeOnMainThread(() => description.Text = value);
    }

    public string MessageTitle
    {
        get => titleLabel.Text;
        set => MainThread.BeginInvokeOnMainThread(() => titleLabel.Text = value);
    }

    public string CancelButton
    {
        get => cancelButton.Text;
        set => MainThread.BeginInvokeOnMainThread(() => cancelButton.Text = value);
    }   
    public ActionAlertPopup()
	{
		InitializeComponent();
	}
    private async void GoBack_Clicked(object sender, EventArgs e)
    {
        await MopupService.Instance.PopAsync();
    }
}