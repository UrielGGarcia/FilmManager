using Wpf.Ui.Controls;

namespace FilmManager.Core.Services;

public class NavigationService
{
    private INavigationView? NavigationControl { get; set; }
    
    public void Navigate(Type pageType)
    {
        NavigationControl?.Navigate(pageType);
    }

    public void SetNavigationControl(INavigationView navigation)
    {
        NavigationControl = navigation;
    }
}