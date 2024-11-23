using Microsoft.Maui.Controls;
using MobileTracking.Pages;
using SimpleToolkit.Core;

namespace MobileTracking;

public partial class AppShell : SimpleToolkit.SimpleShell.SimpleShell
{
    public AppShell()
    {
        InitializeComponent();

        AddTab(typeof(HomePage), PageType.HomePage);
        AddTab(typeof(OrderPage), PageType.OrderPage);
        AddTab(typeof(TrackPage), PageType.TrackPage);
        AddTab(typeof(HistoryPage), PageType.HistoryPage);
        AddTab(typeof(SearchPage), PageType.SearchPage);

        tabBarView.SetSelection(0);

        Loaded += AppShellLoaded;
    }


    private static void AppShellLoaded(object sender, EventArgs e)
    {
        var shell = sender as AppShell;

        shell.Window.SubscribeToSafeAreaChanges(safeArea =>
        {
            shell.tabBarView.Margin = new Thickness(safeArea.Left, safeArea.Top, safeArea.Right, 0);
            shell.tabBarView.TabsPadding = new Thickness(0, 0, 0, safeArea.Bottom);
        });
    }

    private void AddTab(Type page, PageType pageEnum)
    {
        var tab = new Tab { Route = pageEnum.ToString(), Title = pageEnum.ToString() };
        tab.Items.Add(new ShellContent { ContentTemplate = new DataTemplate(page) });

        tabBar.Items.Add(tab);
    }

    private void TabBarViewCurrentPageChanged(object sender, TabBarEventArgs e)
    {
        Shell.Current.GoToAsync("///" + e.CurrentPage.ToString());
    }
}
public enum PageType
{
    HomePage, OrderPage, TrackPage, HistoryPage, SearchPage
}